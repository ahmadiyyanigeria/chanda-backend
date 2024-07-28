using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "CreatedBy", "created_date", "description", "IsDeleted", "ModifiedBy", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("1707d637-97c7-42a8-a009-d8337d238ad1"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("1e07d605-fb48-4bf8-901b-529c913eca20"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("28ba176c-e9b5-4b5b-8690-661086d0ddd2"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("2a32e72f-6538-459e-90fe-86c9e81db29a"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("6517cc00-1a6d-43c8-9439-e4258c1df19a"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("6b999d61-0f45-499c-957e-e45bd9d96992"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("78c7d4b5-c495-49b4-8199-eacdf1d4f307"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" },
                    { new Guid("7f2d487b-3af5-49b4-9d34-60bd53dc509a"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("8b523c47-92bb-4a12-96a4-23b9f6cacf19"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("a40ab152-b68f-424e-b3b3-516a854cccbf"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("badc8256-7352-402f-bf9f-1aa1d86da36f"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("d7c410a2-1ed8-418f-b83a-c336a6be6439"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("e485142f-25f8-4d7d-a2ae-0d2b1fb5125e"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" },
                    { new Guid("ed3a97cf-9a5f-45c3-8849-1be369479b70"), "Admin", new DateTime(2023, 10, 30, 2, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("1707d637-97c7-42a8-a009-d8337d238ad1"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("1e07d605-fb48-4bf8-901b-529c913eca20"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("28ba176c-e9b5-4b5b-8690-661086d0ddd2"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("2a32e72f-6538-459e-90fe-86c9e81db29a"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("6517cc00-1a6d-43c8-9439-e4258c1df19a"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("6b999d61-0f45-499c-957e-e45bd9d96992"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("78c7d4b5-c495-49b4-8199-eacdf1d4f307"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("7f2d487b-3af5-49b4-9d34-60bd53dc509a"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("8b523c47-92bb-4a12-96a4-23b9f6cacf19"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("a40ab152-b68f-424e-b3b3-516a854cccbf"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("badc8256-7352-402f-bf9f-1aa1d86da36f"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("d7c410a2-1ed8-418f-b83a-c336a6be6439"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("e485142f-25f8-4d7d-a2ae-0d2b1fb5125e"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ed3a97cf-9a5f-45c3-8849-1be369479b70"));

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
    }
}
