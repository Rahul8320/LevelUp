using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBorrowingApp.Application.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0073f2dd-3661-47e7-956e-8153ecfc00e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04aa2bdf-ee02-4fa9-b8cb-aa6c969945fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ebee407-3724-4066-8b84-c7cb4020388a");

            migrationBuilder.AddColumn<int>(
                name: "Books_Borrowed",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Books_Lent",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tokens_Available",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "50deb024-e0e5-4a04-be2b-cb9aa495b15c", "2", "User", "USER" },
                    { "79e98253-0ee5-4e81-92c0-8e86367dd92d", "1", "Admin", "ADMIN" },
                    { "ec61b6c6-77f9-423b-954e-6df617c7ee94", "3", "Test", "TEST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50deb024-e0e5-4a04-be2b-cb9aa495b15c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79e98253-0ee5-4e81-92c0-8e86367dd92d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec61b6c6-77f9-423b-954e-6df617c7ee94");

            migrationBuilder.DropColumn(
                name: "Books_Borrowed",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Books_Lent",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Tokens_Available",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0073f2dd-3661-47e7-956e-8153ecfc00e2", "3", "Test", "TEST" },
                    { "04aa2bdf-ee02-4fa9-b8cb-aa6c969945fc", "2", "User", "USER" },
                    { "9ebee407-3724-4066-8b84-c7cb4020388a", "1", "Admin", "ADMIN" }
                });
        }
    }
}
