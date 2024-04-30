using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend_core.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "433cf684-80ab-4d80-9f8b-5a1092b8dd9b", null, "Admin", "ADMIN" },
                    { "46f71574-f4f3-423a-b6b2-35436608e149", null, "User", "USER" },
                    { "f635bbc2-37ef-46b6-bfd4-e003c373bfae", null, "Super Admin", "SUPER_ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "433cf684-80ab-4d80-9f8b-5a1092b8dd9b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46f71574-f4f3-423a-b6b2-35436608e149");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f635bbc2-37ef-46b6-bfd4-e003c373bfae");
        }
    }
}
