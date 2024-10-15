using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("265e29b4-fa66-4e74-9d04-adc3432b4005"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("2c67b3e7-8a79-4b24-b29c-504673bcefc9"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("329e7e95-811d-4785-9647-b83b0a668c79"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("33b0b70b-4390-42fc-89ad-da916b985e28"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("770391fd-d12b-46f4-bafd-4f03f2433f7d"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("7913e605-ffb3-4e9c-9f1b-d3de2dc30be2"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("7cdf008d-a3cf-4909-acf0-75de7bfd13fb"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("9020157f-f50d-480a-a70c-0f2c489e5a86"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("a9e4da59-2d70-4bb2-b907-e34eb22e144b"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("aa8b3ebd-cda4-4b3c-9187-902d7a108dfb"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("af747380-e0b2-4bd0-b206-ac99768387be"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ba15df72-2151-4ed0-bb3a-0136c3f132dc"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("db997308-b260-43a1-a4ae-bebfcfd99e23"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("f629688f-6586-4a45-b23b-7dda5ad18166"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("fee25f54-d742-4bdc-9c5a-a0e2825b661f"));

            migrationBuilder.CreateTable(
                name: "AuditEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityName = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Action = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    UserId = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntries", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "CreatedBy", "created_date", "description", "IsDeleted", "ModifiedBy", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("01d9bc8e-1863-46ce-a08d-e5fcbe4e5342"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" },
                    { new Guid("0e1c9565-7488-4446-8e74-b16306028fa1"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("21e5da11-399b-42f4-87bc-3aff7745dc2e"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" },
                    { new Guid("35f5da1d-2d2e-48e4-be9e-a49d0cce6910"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("44e74844-fce3-49a3-a7d3-17b6c4e82cd4"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("4c7c69a0-2951-42a7-947f-c8998b120df4"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("7792138a-bb9d-4342-b361-188aaf8c2e72"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("784d97f5-beb8-4f8a-9d54-2230c042101a"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("adb35bac-d9b0-4560-9241-8a56cdf99c86"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("c2ea2672-8f63-497e-bb9b-945c4e4a4516"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("c437583c-c8d5-4263-96f3-22d6412a39f7"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("c9690af9-5e91-4d66-80ea-a70b93219e07"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Member.", false, null, null, "Member" },
                    { new Guid("d2910792-3d9c-490f-a5ac-51471f5854d8"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("e16dcb32-e49f-4749-9647-89c52641d980"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("edec0a4b-bd2a-4843-8bbe-76d224f589c3"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditEntries");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("01d9bc8e-1863-46ce-a08d-e5fcbe4e5342"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("0e1c9565-7488-4446-8e74-b16306028fa1"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("21e5da11-399b-42f4-87bc-3aff7745dc2e"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("35f5da1d-2d2e-48e4-be9e-a49d0cce6910"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("44e74844-fce3-49a3-a7d3-17b6c4e82cd4"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("4c7c69a0-2951-42a7-947f-c8998b120df4"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("7792138a-bb9d-4342-b361-188aaf8c2e72"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("784d97f5-beb8-4f8a-9d54-2230c042101a"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("adb35bac-d9b0-4560-9241-8a56cdf99c86"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("c2ea2672-8f63-497e-bb9b-945c4e4a4516"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("c437583c-c8d5-4263-96f3-22d6412a39f7"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("c9690af9-5e91-4d66-80ea-a70b93219e07"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("d2910792-3d9c-490f-a5ac-51471f5854d8"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("e16dcb32-e49f-4749-9647-89c52641d980"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("edec0a4b-bd2a-4843-8bbe-76d224f589c3"));

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "CreatedBy", "created_date", "description", "IsDeleted", "ModifiedBy", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("265e29b4-fa66-4e74-9d04-adc3432b4005"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("2c67b3e7-8a79-4b24-b29c-504673bcefc9"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("329e7e95-811d-4785-9647-b83b0a668c79"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" },
                    { new Guid("33b0b70b-4390-42fc-89ad-da916b985e28"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("770391fd-d12b-46f4-bafd-4f03f2433f7d"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" },
                    { new Guid("7913e605-ffb3-4e9c-9f1b-d3de2dc30be2"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("7cdf008d-a3cf-4909-acf0-75de7bfd13fb"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("9020157f-f50d-480a-a70c-0f2c489e5a86"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("a9e4da59-2d70-4bb2-b907-e34eb22e144b"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Member.", false, null, null, "Member" },
                    { new Guid("aa8b3ebd-cda4-4b3c-9187-902d7a108dfb"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("af747380-e0b2-4bd0-b206-ac99768387be"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("ba15df72-2151-4ed0-bb3a-0136c3f132dc"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("db997308-b260-43a1-a4ae-bebfcfd99e23"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("f629688f-6586-4a45-b23b-7dda5ad18166"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("fee25f54-d742-4bdc-9c5a-a0e2825b661f"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" }
                });
        }
    }
}
