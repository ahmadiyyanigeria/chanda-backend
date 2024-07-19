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

            migrationBuilder.AddColumn<string>(
                name: "role_name",
                table: "member_roles",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "CreatedBy", "created_date", "description", "IsDeleted", "ModifiedBy", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("008d70d7-f9ad-437c-b712-51f4a3fbf79c"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("2f3dfee0-8fff-48d7-80c7-475dc31d71ea"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("3d01cb2f-e35e-42ed-9987-6961687a32e8"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("45309f3b-e865-4834-b831-7690ac4e4a1f"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("4bc1ec80-1e79-487a-b8bd-88439355bae1"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" },
                    { new Guid("50e22c13-ebc3-453f-bedb-66ef5ef05e2d"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("6e0539a9-9020-4eb5-8e34-a44d86ec4a1d"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" },
                    { new Guid("7bee6a19-a5a8-48bc-a15d-27ca5f5de348"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("85cd50d5-d7c3-4223-9689-f2e8403476c7"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("9e61a32a-b4e8-4018-bc22-7d1cec985dda"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("a584bd47-1bc0-4826-a7a8-6a9cad281c64"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Member.", false, null, null, "Member" },
                    { new Guid("aec75f8a-930a-4c25-8597-b2ff873c0b77"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("d06ca577-128f-4702-adc3-5c528bbe75e2"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("e136aaf4-19ee-449d-a0aa-252dce61f2b9"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("ec7366ab-6980-49e0-b6d9-2756c9b8b0a1"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_roles_name",
                table: "roles",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_roles_name",
                table: "roles");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("008d70d7-f9ad-437c-b712-51f4a3fbf79c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("2f3dfee0-8fff-48d7-80c7-475dc31d71ea"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3d01cb2f-e35e-42ed-9987-6961687a32e8"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("45309f3b-e865-4834-b831-7690ac4e4a1f"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("4bc1ec80-1e79-487a-b8bd-88439355bae1"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("50e22c13-ebc3-453f-bedb-66ef5ef05e2d"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("6e0539a9-9020-4eb5-8e34-a44d86ec4a1d"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("7bee6a19-a5a8-48bc-a15d-27ca5f5de348"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("85cd50d5-d7c3-4223-9689-f2e8403476c7"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("9e61a32a-b4e8-4018-bc22-7d1cec985dda"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("a584bd47-1bc0-4826-a7a8-6a9cad281c64"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("aec75f8a-930a-4c25-8597-b2ff873c0b77"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("d06ca577-128f-4702-adc3-5c528bbe75e2"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("e136aaf4-19ee-449d-a0aa-252dce61f2b9"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ec7366ab-6980-49e0-b6d9-2756c9b8b0a1"));

            migrationBuilder.DropColumn(
                name: "role_name",
                table: "member_roles");

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
