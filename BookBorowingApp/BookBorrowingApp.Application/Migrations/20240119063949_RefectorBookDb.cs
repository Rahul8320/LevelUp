using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBorrowingApp.Application.Migrations
{
    /// <inheritdoc />
    public partial class RefectorBookDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b33dab2-d6ea-4f1b-8a80-40469814555b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b23ccc72-1cf2-48f7-b65b-60b448f0c490");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6b49bfa-e92a-40ce-b792-2cf1e13ea13b");

            migrationBuilder.AlterColumn<Guid>(
                name: "Currently_Borrowed_By_User_Id",
                table: "Books",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "Books",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Books",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Books",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "82e6aef5-18b7-43ab-a405-5e05ed105ec7", "1", "Admin", "ADMIN" },
                    { "95465c41-cbbe-4719-9ac5-9e19dd186c0b", "3", "Test", "TEST" },
                    { "daab2038-2447-44b3-986b-1cde34ff494c", "2", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82e6aef5-18b7-43ab-a405-5e05ed105ec7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95465c41-cbbe-4719-9ac5-9e19dd186c0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "daab2038-2447-44b3-986b-1cde34ff494c");

            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Books");

            migrationBuilder.AlterColumn<Guid>(
                name: "Currently_Borrowed_By_User_Id",
                table: "Books",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b33dab2-d6ea-4f1b-8a80-40469814555b", "1", "Admin", "ADMIN" },
                    { "b23ccc72-1cf2-48f7-b65b-60b448f0c490", "3", "Test", "TEST" },
                    { "e6b49bfa-e92a-40ce-b792-2cf1e13ea13b", "2", "User", "USER" }
                });
        }
    }
}
