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
                keyValue: new Guid("115a8582-e670-4230-9535-782cf2a3a30d"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("28a78577-dd81-4a59-82e9-ce9aa3d6fb7a"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("3739f033-fee3-4111-a3f5-d56bd17d31a6"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("380ba5d3-71c0-4237-b12e-10ab73e4bdd0"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("4b62c123-a7a6-476d-97ca-0899ed82db8a"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("5fa75c9a-f31b-4ec9-8d5a-6b97305ca972"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("81d38557-fae1-4495-b036-a2789027aa4d"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("8a1f7386-2a1e-40ac-9f78-63c4877cfb37"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("a241315f-fbe7-4771-9be9-d10003725249"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("c57d801c-8164-46fc-b02f-114259f596c6"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("cbdec0c8-22d6-45fb-b60c-da3422e4766b"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("e9bcf08a-8654-4bfe-9f4d-6adc34b3f747"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ec9d1db7-751c-4b15-a5a8-1d0a6accc877"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("fbd6f007-ffa4-4baa-b192-f43f427f0e45"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("fd29927c-bcbd-4cf0-9921-3806439105f9"));

            migrationBuilder.AddColumn<string>(
                name: "reference",
                table: "payments",
                type: "text",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "chanda_type",
                type: "varchar(50)",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldCollation: "case_insensitive");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "CreatedBy", "created_date", "description", "IsDeleted", "ModifiedBy", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("08d9f2e3-fbe3-409c-afee-e8a82bc3e43c"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Member.", false, null, null, "Member" },
                    { new Guid("294beda7-440f-4b4f-81d9-d3db2d155293"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("411424df-e386-4e5f-8826-ac3fc6a77f73"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("41e47caf-3185-46c5-a25f-1213294300b4"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("48a41ac5-2c07-4f1f-a611-a4543374e42e"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("6191bec4-2e42-4a1e-bd40-cbb4333f5198"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" },
                    { new Guid("723fcf82-666b-4681-8413-06ad73467bdd"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("9c8d67e6-125b-41f8-b7c2-ac462dfbc4b0"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("a4d52cd0-b8c7-466e-bff1-033de8fd76d7"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" },
                    { new Guid("ac1b621b-9940-4f1f-965d-5d6251b88313"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("adb8caef-eb15-4b0b-b82a-2e3a89d42860"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("b446e28f-7d0e-45ca-a7aa-581337f5f1f8"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("d7add587-e3c9-423b-9cd1-f8fa2db567ff"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("e2ff8117-da7a-449d-9f28-1e2f311e9092"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("ec60d446-4572-49cf-af27-b1e949e90a2d"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_payments_reference",
                table: "payments",
                column: "reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_chanda_type_name",
                table: "chanda_type",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_payments_reference",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_chanda_type_name",
                table: "chanda_type");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("08d9f2e3-fbe3-409c-afee-e8a82bc3e43c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("294beda7-440f-4b4f-81d9-d3db2d155293"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("411424df-e386-4e5f-8826-ac3fc6a77f73"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("41e47caf-3185-46c5-a25f-1213294300b4"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("48a41ac5-2c07-4f1f-a611-a4543374e42e"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("6191bec4-2e42-4a1e-bd40-cbb4333f5198"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("723fcf82-666b-4681-8413-06ad73467bdd"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("9c8d67e6-125b-41f8-b7c2-ac462dfbc4b0"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("a4d52cd0-b8c7-466e-bff1-033de8fd76d7"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ac1b621b-9940-4f1f-965d-5d6251b88313"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("adb8caef-eb15-4b0b-b82a-2e3a89d42860"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("b446e28f-7d0e-45ca-a7aa-581337f5f1f8"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("d7add587-e3c9-423b-9cd1-f8fa2db567ff"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("e2ff8117-da7a-449d-9f28-1e2f311e9092"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("ec60d446-4572-49cf-af27-b1e949e90a2d"));

            migrationBuilder.DropColumn(
                name: "reference",
                table: "payments");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "chanda_type",
                type: "varchar(255)",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldCollation: "case_insensitive");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "CreatedBy", "created_date", "description", "IsDeleted", "ModifiedBy", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("115a8582-e670-4230-9535-782cf2a3a30d"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit Financial Secretary.", false, null, null, "Circuit-Fin-Sec" },
                    { new Guid("28a78577-dd81-4a59-82e9-ce9aa3d6fb7a"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Vice Circuit President.", false, null, null, "VCP" },
                    { new Guid("3739f033-fee3-4111-a3f5-d56bd17d31a6"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat General Secretary.", false, null, null, "Jamaat-Gen-Sec" },
                    { new Guid("380ba5d3-71c0-4237-b12e-10ab73e4bdd0"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat President.", false, null, null, "Jamaat-President" },
                    { new Guid("4b62c123-a7a6-476d-97ca-0899ed82db8a"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National General Secretary.", false, null, null, "Nationa-Gen-Sec" },
                    { new Guid("5fa75c9a-f31b-4ec9-8d5a-6b97305ca972"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Acting Head of AMJN", false, null, null, "Acting-Amir" },
                    { new Guid("81d38557-fae1-4495-b036-a2789027aa4d"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Head of AMJN", false, null, null, "Amir" },
                    { new Guid("8a1f7386-2a1e-40ac-9f78-63c4877cfb37"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Tajneed Secretary", false, null, null, "National-Tajneed" },
                    { new Guid("a241315f-fbe7-4771-9be9-d10003725249"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit General Secretary.", false, null, null, "Circuit-Gen-Sec" },
                    { new Guid("c57d801c-8164-46fc-b02f-114259f596c6"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Financial Secretary.", false, null, null, "Jamaat-Fin-Sec" },
                    { new Guid("cbdec0c8-22d6-45fb-b60c-da3422e4766b"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Jamaat Member.", false, null, null, "Member" },
                    { new Guid("e9bcf08a-8654-4bfe-9f4d-6adc34b3f747"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Circuit President.", false, null, null, "CP" },
                    { new Guid("ec9d1db7-751c-4b15-a5a8-1d0a6accc877"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "National Financial Secretary", false, null, null, "National-Fin-Sec" },
                    { new Guid("fbd6f007-ffa4-4baa-b192-f43f427f0e45"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Full administrative access across the system.", false, null, null, "Admin" },
                    { new Guid("fd29927c-bcbd-4cf0-9921-3806439105f9"), "Admin", new DateTime(2024, 1, 1, 3, 10, 28, 488, DateTimeKind.Utc), "Naib Amir", false, null, null, "Naib-Amir" }
                });
        }
    }
}
