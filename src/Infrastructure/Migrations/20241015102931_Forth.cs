using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Forth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "AuditEntries",
                type: "text",
                nullable: true,
                collation: "case_insensitive");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "CreatedBy", "created_date", "description", "IsDeleted", "ModifiedBy", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("02d7afb6-0c9f-4dbd-81d6-9ba7ac73d990"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("0a35100b-6c47-478c-a8bd-7bd432808997"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("19cebb0a-b808-4ba8-8492-c294c2334adf"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("2815b904-3c80-4ba0-ba19-3ebe0864f043"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" },
                    { new Guid("31905e67-ba67-47c7-826a-1364e7454188"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" },
                    { new Guid("8650e6c2-cdba-4f8e-ac82-b3f5a7cda879"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("a0855ac7-1b38-4c93-92c0-92fe3b4e4b88"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("baf2651e-05e1-4c74-91d3-ef4b98b5489e"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("c1d434b3-30e6-43df-8ac0-e2a8d5def920"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" },
                    { new Guid("cb02daed-c4b9-4a43-a5aa-ad62b70f0d13"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("d1c3f9a9-96f1-4bb9-aed2-37d2003c74dc"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("d3d48071-072b-4829-a9f4-582e42a863f0"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("e311161c-57a9-45c6-b0fe-a98acf3d4a38"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("e7e48fac-c88e-4bcd-a563-fa2194fe3b6c"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("f320b997-3091-4652-b2c2-0de4ecc98fb0"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Member.", false, null, null, "Member" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("02d7afb6-0c9f-4dbd-81d6-9ba7ac73d990"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("0a35100b-6c47-478c-a8bd-7bd432808997"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("19cebb0a-b808-4ba8-8492-c294c2334adf"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("2815b904-3c80-4ba0-ba19-3ebe0864f043"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("31905e67-ba67-47c7-826a-1364e7454188"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("8650e6c2-cdba-4f8e-ac82-b3f5a7cda879"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("a0855ac7-1b38-4c93-92c0-92fe3b4e4b88"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("baf2651e-05e1-4c74-91d3-ef4b98b5489e"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("c1d434b3-30e6-43df-8ac0-e2a8d5def920"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("cb02daed-c4b9-4a43-a5aa-ad62b70f0d13"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("d1c3f9a9-96f1-4bb9-aed2-37d2003c74dc"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("d3d48071-072b-4829-a9f4-582e42a863f0"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("e311161c-57a9-45c6-b0fe-a98acf3d4a38"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("e7e48fac-c88e-4bcd-a563-fa2194fe3b6c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("f320b997-3091-4652-b2c2-0de4ecc98fb0"));

            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "AuditEntries");

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
    }
}
