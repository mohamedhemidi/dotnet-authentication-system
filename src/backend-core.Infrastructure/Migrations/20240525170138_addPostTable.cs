using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend_core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c8f5820-ecca-42b8-becd-f91a804a8352");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c499c00c-da8a-427f-8131-64d6fef110a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7672747-09b1-4418-9cb3-eddbce2e3959");

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Body = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Created_by = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Updated_by = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c4b83bb-400e-4f9d-8133-1430b8e113e1", null, "Admin", "ADMIN" },
                    { "67625991-0809-468c-9302-ef58525e92d8", null, "Super Admin", "SUPER_ADMIN" },
                    { "a48ca5cd-4c51-4c71-97ba-099fd95d5980", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c4b83bb-400e-4f9d-8133-1430b8e113e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67625991-0809-468c-9302-ef58525e92d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a48ca5cd-4c51-4c71-97ba-099fd95d5980");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c8f5820-ecca-42b8-becd-f91a804a8352", null, "User", "USER" },
                    { "c499c00c-da8a-427f-8131-64d6fef110a1", null, "Admin", "ADMIN" },
                    { "f7672747-09b1-4418-9cb3-eddbce2e3959", null, "Super Admin", "SUPER_ADMIN" }
                });
        }
    }
}
