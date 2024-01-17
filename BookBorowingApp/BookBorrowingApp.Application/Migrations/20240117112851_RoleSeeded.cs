using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBorrowingApp.Application.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a7327ede-d311-4cb9-881b-fd23e091180a", "2", "User", "User" },
                    { "aa3f8299-b5d8-4c86-8c40-5aa5ad3fa0c0", "1", "Admin", "Admin" },
                    { "d395366a-ab3c-47f0-bed5-26456045a191", "3", "Test", "Test User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7327ede-d311-4cb9-881b-fd23e091180a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa3f8299-b5d8-4c86-8c40-5aa5ad3fa0c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d395366a-ab3c-47f0-bed5-26456045a191");
        }
    }
}
