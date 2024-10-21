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
                name: "AuditEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityName = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Action = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    ActionName = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    UserId = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    UserName = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chanda_type",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    code = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
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
                    jamaat_ledger_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
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
                    reference = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    option = table.Column<string>(type: "varchar(50)", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", nullable: false),
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
                    jamaat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    invoice_id = table.Column<Guid>(type: "uuid", nullable: false),
                    receipt_no = table.Column<string>(type: "text", nullable: true, collation: "case_insensitive"),
                    reference_no = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
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
                        name: "FK_invoice_items_jamaats_jamaat_id",
                        column: x => x.jamaat_id,
                        principalTable: "jamaats",
                        principalColumn: "Id",
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
                name: "reminders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    member_id = table.Column<Guid>(type: "uuid", nullable: false),
                    day = table.Column<int>(type: "integer", nullable: false),
                    hour = table.Column<int>(type: "integer", nullable: false),
                    minute = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    cron_expression = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    reminder_title = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    description = table.Column<string>(type: "varchar(255)", nullable: true, collation: "case_insensitive"),
                    via_sms = table.Column<bool>(type: "boolean", nullable: false),
                    via_mail = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true, collation: "case_insensitive"),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reminders", x => x.id);
                    table.ForeignKey(
                        name: "FK_reminders_members_member_id",
                        column: x => x.member_id,
                        principalTable: "members",
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
                    { new Guid("2100e19d-699a-4ce2-be0b-ac7f56cc59d2"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("27d159c7-310a-449d-ab22-a3bcf2f3e585"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" },
                    { new Guid("54e5e167-1ce3-49a7-b372-76e31af6326c"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("63f17b42-e04f-44ff-a38e-d0808125a45f"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" },
                    { new Guid("67e91d08-6237-49a1-98f4-b18ddf693520"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("9810b39a-3a25-4204-aee7-bb093df6f13a"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("99f78295-f3c2-417b-a2b0-d9e9eedc7210"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("9ff7d3e5-e32b-4528-8a4d-eaa57a561c33"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("a8e40cba-51c9-45de-83c8-3e1d1fb415d7"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Member.", false, null, null, "Member" },
                    { new Guid("b19f4a12-5fcb-408f-9b81-e7001fc0d0bf"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("cbaf21ea-a91f-4e41-b89e-521ce5cc0d4b"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("d38c87ba-e03c-4844-a590-cef509a992b0"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("da2712fa-6181-4f4c-a572-db014eac3dc3"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("fc384e77-9456-4176-b3f8-aa7b5043dcb5"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("fe8e4f67-6212-47b7-bb90-b6fbf52c0ef0"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" }
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
                name: "IX_chanda_type_name",
                table: "chanda_type",
                column: "name",
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
                name: "IX_invoice_items_jamaat_id",
                table: "invoice_items",
                column: "jamaat_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_items_payer_id",
                table: "invoice_items",
                column: "payer_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_items_receipt_no",
                table: "invoice_items",
                column: "receipt_no",
                unique: true);

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
                column: "jamaat_id",
                unique: true);

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
                name: "IX_payments_reference",
                table: "payments",
                column: "reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reminders_member_id",
                table: "reminders",
                column: "member_id");

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
                name: "AuditEntries");

            migrationBuilder.DropTable(
                name: "chanda_items");

            migrationBuilder.DropTable(
                name: "ledgers");

            migrationBuilder.DropTable(
                name: "member_roles");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "reminders");

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
