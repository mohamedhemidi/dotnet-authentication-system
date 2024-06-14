using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend_core.Infrastructure.Migrations.MySQL
{
    /// <inheritdoc />
    public partial class AddRefreshTokenColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33ef03f1-6596-48cf-ad5a-7427df5b7ec9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "751cefdc-01f4-40d1-810d-a235d2d63312");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0c2c565-3372-4b12-a624-d289aeed6fc1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e43033a2-670a-468d-ab80-0fc361ea1dc9");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "065a8524-f3ea-4295-ab5c-f9a60bb32077", null, "Super Admin", "SUPER_ADMIN" },
                    { "1055a263-5c3d-4074-a669-66e39595ab27", null, "Moderator", "MODERATOR" },
                    { "2d666648-38fe-4968-be10-300c2e9ea7a6", null, "Admin", "ADMIN" },
                    { "c22575cf-c333-42a2-ae37-54ec23b9347a", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "065a8524-f3ea-4295-ab5c-f9a60bb32077");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1055a263-5c3d-4074-a669-66e39595ab27");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d666648-38fe-4968-be10-300c2e9ea7a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c22575cf-c333-42a2-ae37-54ec23b9347a");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33ef03f1-6596-48cf-ad5a-7427df5b7ec9", null, "User", "USER" },
                    { "751cefdc-01f4-40d1-810d-a235d2d63312", null, "Admin", "ADMIN" },
                    { "a0c2c565-3372-4b12-a624-d289aeed6fc1", null, "Moderator", "MODERATOR" },
                    { "e43033a2-670a-468d-ab80-0fc361ea1dc9", null, "Super Admin", "SUPER_ADMIN" }
                });
        }
    }
}
