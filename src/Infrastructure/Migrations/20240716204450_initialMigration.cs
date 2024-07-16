using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:case_insensitive", "en-u-ks-primary,en-u-ks-primary,icu,False");

            migrationBuilder.CreateTable(
                name: "chanda_type",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    code = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    description = table.Column<string>(type: "text", nullable: true, collation: "case_insensitive"),
                    income_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chanda_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "circuits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(120)", nullable: false, collation: "case_insensitive"),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_circuits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    description = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "jamaats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(120)", nullable: false, collation: "case_insensitive"),
                    circuit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jamaats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_jamaats_circuits_circuit_id",
                        column: x => x.circuit_id,
                        principalTable: "circuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    jamaat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.id);
                    table.ForeignKey(
                        name: "FK_invoices_jamaats_jamaat_id",
                        column: x => x.jamaat_id,
                        principalTable: "jamaats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "jamaat_ledgers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    jamaat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jamaat_ledgers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_jamaat_ledgers_jamaats_jamaat_id",
                        column: x => x.jamaat_id,
                        principalTable: "jamaats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chanda_no = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    name = table.Column<string>(type: "varchar(120)", nullable: false, collation: "case_insensitive"),
                    email = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    phone_no = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    jamaat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    member_ledger_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.id);
                    table.ForeignKey(
                        name: "FK_members_jamaats_jamaat_id",
                        column: x => x.jamaat_id,
                        principalTable: "jamaats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    invoice_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    option = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.id);
                    table.ForeignKey(
                        name: "FK_payments_invoices_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "invoices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    payer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    invoice_id = table.Column<Guid>(type: "uuid", nullable: false),
                    month_paid_for = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_invoice_items_invoices_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "invoices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_invoice_items_members_payer_id",
                        column: x => x.payer_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "member_ledgers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    member_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_ledgers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_member_ledgers_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "member_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_member_roles_members_user_id",
                        column: x => x.user_id,
                        principalTable: "members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_member_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chanda_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    invoice_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    chanda_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chanda_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_chanda_items_chanda_type_chanda_type_id",
                        column: x => x.chanda_type_id,
                        principalTable: "chanda_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chanda_items_invoice_items_invoice_item_id",
                        column: x => x.invoice_item_id,
                        principalTable: "invoice_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ledgers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chanda_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    month_paid_for = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    entry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    member_ledger_id = table.Column<Guid>(type: "uuid", nullable: true),
                    jamaat_ledger_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    modified_by = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ledgers", x => x.id);
                    table.ForeignKey(
                        name: "FK_ledgers_chanda_type_chanda_type_id",
                        column: x => x.chanda_type_id,
                        principalTable: "chanda_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ledgers_jamaat_ledgers_jamaat_ledger_id",
                        column: x => x.jamaat_ledger_id,
                        principalTable: "jamaat_ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ledgers_member_ledgers_member_ledger_id",
                        column: x => x.member_ledger_id,
                        principalTable: "member_ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "CreatedBy", "created_date", "description", "IsDeleted", "ModifiedBy", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("00a7cb15-9762-49bf-88ce-9b89bb0d9587"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("060b9e02-3113-4f7e-9e01-ede8e05ae8b0"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("063e320d-998a-4227-8a9c-55bda812c80d"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" },
                    { new Guid("109c43af-1f5d-4b97-a277-b4f03f19fe09"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" },
                    { new Guid("15838d8e-f451-4118-8068-e4940e6d411a"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("4392e690-f0cf-4a8b-822f-148823b2889d"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("4aad7c2c-f22b-44e4-80fe-e46e7481acf1"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" },
                    { new Guid("55c5a85d-2e98-4311-a55d-e021bcbe08cc"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("a69ca44c-d909-492e-9fa5-5a7f00dbef50"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("ae27aa2d-679b-42a4-82cb-c917cb677082"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("afc51b35-d850-4819-a75c-7ea97de10eb2"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("bb9c2722-2a22-41bb-8944-de7170f6e604"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("dd8a5d26-46e6-402f-afb1-3fd14b662fef"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("e54bc127-3e25-4993-9060-fd593d1d3619"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_chanda_items_chanda_type_id",
                table: "chanda_items",
                column: "chanda_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_chanda_items_invoice_item_id",
                table: "chanda_items",
                column: "invoice_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_chanda_type_code",
                table: "chanda_type",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_circuits_name",
                table: "circuits",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoice_items_invoice_id",
                table: "invoice_items",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_items_payer_id",
                table: "invoice_items",
                column: "payer_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_jamaat_id",
                table: "invoices",
                column: "jamaat_id");

            migrationBuilder.CreateIndex(
                name: "IX_jamaat_ledgers_jamaat_id",
                table: "jamaat_ledgers",
                column: "jamaat_id");

            migrationBuilder.CreateIndex(
                name: "IX_jamaats_circuit_id",
                table: "jamaats",
                column: "circuit_id");

            migrationBuilder.CreateIndex(
                name: "IX_ledgers_chanda_type_id",
                table: "ledgers",
                column: "chanda_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_ledgers_jamaat_ledger_id",
                table: "ledgers",
                column: "jamaat_ledger_id");

            migrationBuilder.CreateIndex(
                name: "IX_ledgers_member_ledger_id",
                table: "ledgers",
                column: "member_ledger_id");

            migrationBuilder.CreateIndex(
                name: "IX_member_ledgers_member_id",
                table: "member_ledgers",
                column: "member_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_member_roles_role_id",
                table: "member_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_member_roles_user_id",
                table: "member_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_members_chanda_no",
                table: "members",
                column: "chanda_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_members_jamaat_id",
                table: "members",
                column: "jamaat_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_invoice_id",
                table: "payments",
                column: "invoice_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chanda_items");

            migrationBuilder.DropTable(
                name: "ledgers");

            migrationBuilder.DropTable(
                name: "member_roles");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "invoice_items");

            migrationBuilder.DropTable(
                name: "chanda_type");

            migrationBuilder.DropTable(
                name: "jamaat_ledgers");

            migrationBuilder.DropTable(
                name: "member_ledgers");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "jamaats");

            migrationBuilder.DropTable(
                name: "circuits");
        }
    }
}
