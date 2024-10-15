using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AuditEntries",
                type: "text",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "CreatedBy", "created_date", "description", "IsDeleted", "ModifiedBy", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("1336eb13-01b6-4d03-9570-af67c62b9c58"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("3292a74b-d86b-48a0-990d-770484f0f9eb"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("3aa71b7b-9d22-4909-9b79-fc2bc271f575"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("4a7035c9-60a2-43f2-b212-f7a940408a63"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("800c8add-3a8a-4bfe-ae52-e460ee65d1fd"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" },
                    { new Guid("8324ac03-8b9c-4f62-a8f4-66065a06d967"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" },
                    { new Guid("a7c64847-7207-40d5-8f93-9bd84a97e2dc"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("b8338a36-a017-4861-b42c-b61ba4d79d28"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("bc7daf21-9942-4f6e-8784-8d10ecd0115f"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" },
                    { new Guid("bdb81bb9-3758-4b18-9c38-52d6bc823220"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Member.", false, null, null, "Member" },
                    { new Guid("e7847da0-2664-474f-8b0f-2a949adf4a0c"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("e886a61c-5942-406f-9c8d-a2c157fb4684"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("ea70d01e-8770-4c26-acdf-b121eae298cc"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("fb003223-8854-4666-a2fe-6a29b55dced0"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("fc9fa73c-c635-45ef-8d71-d62b4f0f8434"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("1336eb13-01b6-4d03-9570-af67c62b9c58"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3292a74b-d86b-48a0-990d-770484f0f9eb"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3aa71b7b-9d22-4909-9b79-fc2bc271f575"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("4a7035c9-60a2-43f2-b212-f7a940408a63"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("800c8add-3a8a-4bfe-ae52-e460ee65d1fd"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("8324ac03-8b9c-4f62-a8f4-66065a06d967"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("a7c64847-7207-40d5-8f93-9bd84a97e2dc"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("b8338a36-a017-4861-b42c-b61ba4d79d28"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("bc7daf21-9942-4f6e-8784-8d10ecd0115f"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("bdb81bb9-3758-4b18-9c38-52d6bc823220"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("e7847da0-2664-474f-8b0f-2a949adf4a0c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("e886a61c-5942-406f-9c8d-a2c157fb4684"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ea70d01e-8770-4c26-acdf-b121eae298cc"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("fb003223-8854-4666-a2fe-6a29b55dced0"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("fc9fa73c-c635-45ef-8d71-d62b4f0f8434"));

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AuditEntries");

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
    }
}
