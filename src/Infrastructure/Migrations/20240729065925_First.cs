using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
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
                    Code = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
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
                    Code = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
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
                    reference = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", nullable: false),
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
                    option = table.Column<string>(type: "varchar(50)", nullable: false),
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
                    month_paid_for = table.Column<string>(type: "varchar(50)", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
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
                    role_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
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
                    month_paid_for = table.Column<string>(type: "varchar(50)", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
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
                    { new Guid("088f16f8-aca5-4a2a-9dd5-5631221faf76"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("0cbf479f-7e7c-4a01-8249-8f07385e4687"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("205eebb0-d95e-477c-9664-3de3000ac213"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("20deee83-1eb7-4bd3-a262-986b3d5b95d2"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("2153623f-81e5-4bca-8eb9-655f4dc89ae4"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("860807c4-4baf-4f6b-bba3-aef471aba92a"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" },
                    { new Guid("8c0f0ae9-bee1-416c-9148-3269bc49398e"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Member.", false, null, null, "Member" },
                    { new Guid("91189008-95dc-49da-8c5b-3e309d70e03d"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" },
                    { new Guid("a125c4ef-da57-4a63-a3e2-6e5c35ab0419"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("ae0a429a-685e-422c-9f44-e44cadeca6c1"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" },
                    { new Guid("b62041e0-d853-46dc-821f-98121a99dfdf"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("c0f9c93c-f225-4ce4-ae28-8db5859faedb"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("d57967c7-611b-40f1-9252-c1a1517de26e"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("d9b67088-0566-44f8-a7bf-a50c5af2fe39"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("e3b0da53-2bcb-4745-aa0a-d95234dc2bcf"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" }
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
                name: "IX_invoices_reference",
                table: "invoices",
                column: "reference",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_roles_name",
                table: "roles",
                column: "name",
                unique: true);
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
