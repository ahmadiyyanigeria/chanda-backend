using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("00a7cb15-9762-49bf-88ce-9b89bb0d9587"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("060b9e02-3113-4f7e-9e01-ede8e05ae8b0"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("063e320d-998a-4227-8a9c-55bda812c80d"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("109c43af-1f5d-4b97-a277-b4f03f19fe09"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("15838d8e-f451-4118-8068-e4940e6d411a"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("4392e690-f0cf-4a8b-822f-148823b2889d"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("4aad7c2c-f22b-44e4-80fe-e46e7481acf1"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("55c5a85d-2e98-4311-a55d-e021bcbe08cc"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("a69ca44c-d909-492e-9fa5-5a7f00dbef50"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ae27aa2d-679b-42a4-82cb-c917cb677082"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("afc51b35-d850-4819-a75c-7ea97de10eb2"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("bb9c2722-2a22-41bb-8944-de7170f6e604"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("dd8a5d26-46e6-402f-afb1-3fd14b662fef"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("e54bc127-3e25-4993-9060-fd593d1d3619"));

            migrationBuilder.AlterColumn<string>(
                name: "option",
                table: "payments",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "invoices",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "CreatedBy", "created_date", "description", "IsDeleted", "ModifiedBy", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("09db637a-07ea-4488-a78b-a2f52ec0d1cd"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("148e1dae-fd66-4175-9d5e-4586820452e1"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("2ab3146b-b006-4556-b511-489ae61fbb36"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("2b0ee60c-aa73-40c9-b876-0fbdab84755c"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("2ba5f679-4f0d-49d2-946e-d8b1a56f0c63"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("39edd755-1045-42ec-9bff-4eab330761f6"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("53167742-b6df-44f2-94b4-3c8f70b2bc0f"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("70c7596e-eed4-4fe0-9d56-12e5e4a8c1d2"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("8b010dc8-4617-4aaa-8e9c-25d15c813171"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" },
                    { new Guid("ba2754fe-32be-4b8d-8965-75eae25d6c5d"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("c0e1e9ef-9cd0-429b-a8fb-ecf4261165b6"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" },
                    { new Guid("c721467c-11ab-4345-8bf5-69903eababad"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" },
                    { new Guid("ca90b66b-893a-49e7-a87f-ec67b13c8ab7"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("e951f38e-9a94-4fa6-bb65-99aa40d4917e"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("09db637a-07ea-4488-a78b-a2f52ec0d1cd"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("148e1dae-fd66-4175-9d5e-4586820452e1"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("2ab3146b-b006-4556-b511-489ae61fbb36"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("2b0ee60c-aa73-40c9-b876-0fbdab84755c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("2ba5f679-4f0d-49d2-946e-d8b1a56f0c63"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("39edd755-1045-42ec-9bff-4eab330761f6"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("53167742-b6df-44f2-94b4-3c8f70b2bc0f"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("70c7596e-eed4-4fe0-9d56-12e5e4a8c1d2"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("8b010dc8-4617-4aaa-8e9c-25d15c813171"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ba2754fe-32be-4b8d-8965-75eae25d6c5d"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("c0e1e9ef-9cd0-429b-a8fb-ecf4261165b6"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("c721467c-11ab-4345-8bf5-69903eababad"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ca90b66b-893a-49e7-a87f-ec67b13c8ab7"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("e951f38e-9a94-4fa6-bb65-99aa40d4917e"));

            migrationBuilder.AlterColumn<int>(
                name: "option",
                table: "payments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "invoices",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

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
        }
    }
}
