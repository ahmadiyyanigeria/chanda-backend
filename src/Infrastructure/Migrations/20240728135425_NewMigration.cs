using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "month_paid_for",
                table: "ledgers",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldCollation: "case_insensitive");

            migrationBuilder.AddColumn<int>(
                name: "year",
                table: "ledgers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "invoices",
                type: "text",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "month_paid_for",
                table: "invoice_items",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldCollation: "case_insensitive");

            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                table: "invoice_items",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "year",
                table: "invoice_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "CreatedBy", "created_date", "description", "IsDeleted", "ModifiedBy", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("054120db-9030-4b54-92cd-c885635a8882"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("09a484a5-0d30-4ae7-884a-dd941a778d34"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("0ed45e55-3338-4a1c-92a4-74b57260b98e"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("18ff89ea-9c44-4033-a404-a5f9aa71078c"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("406c8c92-dc1f-42b2-b378-16daa81b6b55"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" },
                    { new Guid("65cc2d31-3277-436d-a89c-92cc94fa33ec"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" },
                    { new Guid("754f5bf8-8a1c-4d91-8309-4311922d6773"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Member.", false, null, null, "Member" },
                    { new Guid("8e1d7c5d-194a-4004-ba4d-53215c822d3f"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("996f140c-7569-4f85-afd4-d490658267b3"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("a1356736-ab3e-404e-a6f4-f70b9a373a8b"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("aef29cbe-a325-4257-a15d-1287918a0014"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("b827504d-6726-4a1d-a477-934e486b6b85"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("c319d9bc-8cf9-4547-a09a-9504536d76df"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" },
                    { new Guid("c7068905-27c5-4651-9980-2c7ca1dba7be"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("df154b51-12db-4e07-a7ba-a57a739cdb41"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("054120db-9030-4b54-92cd-c885635a8882"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("09a484a5-0d30-4ae7-884a-dd941a778d34"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("0ed45e55-3338-4a1c-92a4-74b57260b98e"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("18ff89ea-9c44-4033-a404-a5f9aa71078c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("406c8c92-dc1f-42b2-b378-16daa81b6b55"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("65cc2d31-3277-436d-a89c-92cc94fa33ec"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("754f5bf8-8a1c-4d91-8309-4311922d6773"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("8e1d7c5d-194a-4004-ba4d-53215c822d3f"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("996f140c-7569-4f85-afd4-d490658267b3"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("a1356736-ab3e-404e-a6f4-f70b9a373a8b"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("aef29cbe-a325-4257-a15d-1287918a0014"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("b827504d-6726-4a1d-a477-934e486b6b85"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("c319d9bc-8cf9-4547-a09a-9504536d76df"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("c7068905-27c5-4651-9980-2c7ca1dba7be"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("df154b51-12db-4e07-a7ba-a57a739cdb41"));

            migrationBuilder.DropColumn(
                name: "year",
                table: "ledgers");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "invoices");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "invoice_items");

            migrationBuilder.DropColumn(
                name: "year",
                table: "invoice_items");

            migrationBuilder.AlterColumn<string>(
                name: "month_paid_for",
                table: "ledgers",
                type: "varchar(50)",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "month_paid_for",
                table: "invoice_items",
                type: "varchar(50)",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(50)");

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
        }
    }
}
