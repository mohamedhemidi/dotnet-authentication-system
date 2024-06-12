using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend_core.Infrastructure.Migrations.MySQL
{
    /// <inheritdoc />
    public partial class InitMysql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21310832-291b-41e1-a0fa-0b5c49052ba9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a098176-8c4c-40b5-b91b-179a41c9179b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cab38bd-cecb-40f1-bf45-e09e0fa2c5b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cce84a9d-3b36-454a-8b08-6e927b0f41c9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "74b5bec7-ae43-4be0-8390-d0e5145a51d7", null, "User", "USER" },
                    { "aa10e9ae-d0bd-44c7-b931-1f080a464ede", null, "Moderator", "MODERATOR" },
                    { "b8df49c4-9083-41f4-a1c7-4707248b19ea", null, "Super Admin", "SUPER_ADMIN" },
                    { "c06ea4d9-8001-462d-ba83-3fd9d61ac3a1", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74b5bec7-ae43-4be0-8390-d0e5145a51d7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa10e9ae-d0bd-44c7-b931-1f080a464ede");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8df49c4-9083-41f4-a1c7-4707248b19ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c06ea4d9-8001-462d-ba83-3fd9d61ac3a1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21310832-291b-41e1-a0fa-0b5c49052ba9", null, "Moderator", "MODERATOR" },
                    { "3a098176-8c4c-40b5-b91b-179a41c9179b", null, "User", "USER" },
                    { "9cab38bd-cecb-40f1-bf45-e09e0fa2c5b4", null, "Admin", "ADMIN" },
                    { "cce84a9d-3b36-454a-8b08-6e927b0f41c9", null, "Super Admin", "SUPER_ADMIN" }
                });
        }
    }
}
