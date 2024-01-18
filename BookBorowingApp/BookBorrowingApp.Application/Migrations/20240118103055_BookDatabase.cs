using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBorrowingApp.Application.Migrations
{
    /// <inheritdoc />
    public partial class BookDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Books_Lent",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Books_Borrowed",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    Is_Book_Available = table.Column<bool>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Lent_By_User_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Currently_Borrowed_By_User_Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "83298bfd-a7e6-4fd5-beed-c408725b7c38", "2", "User", "USER" },
                    { "93bc889c-77bc-4ac7-a485-6fc308767d12", "1", "Admin", "ADMIN" },
                    { "de9c54de-ac90-4ef8-b08a-89aa545b476c", "3", "Test", "TEST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83298bfd-a7e6-4fd5-beed-c408725b7c38");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93bc889c-77bc-4ac7-a485-6fc308767d12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de9c54de-ac90-4ef8-b08a-89aa545b476c");

            migrationBuilder.AlterColumn<int>(
                name: "Books_Lent",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Books_Borrowed",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

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
    }
}
