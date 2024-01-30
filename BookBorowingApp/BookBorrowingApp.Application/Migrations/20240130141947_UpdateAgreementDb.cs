using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBorrowingApp.Application.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAgreementDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8614b66d-743f-4a6e-8c9f-7de730680959");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c00b4910-470e-4117-a006-cbfdf31c07a7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d3011c96-f1fb-40df-b58f-b0b1c1346923", "05f0dc92-33e5-4a99-bf6b-44847148f19c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3011c96-f1fb-40df-b58f-b0b1c1346923");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05f0dc92-33e5-4a99-bf6b-44847148f19c");

            migrationBuilder.AddColumn<bool>(
                name: "IsAcceptedByUser",
                table: "Agreements",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25bb5472-ad64-47ca-a605-06f0b78c5687", null, "Test", "TEST" },
                    { "41329b3d-4536-40b4-8fc3-50af5d86affe", null, "User", "USER" },
                    { "dd480c93-5329-45d3-8269-ac2582a70f81", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Books_Borrowed", "Books_Lent", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tokens_Available", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5d074a2d-7b00-4156-8636-f3a9e6cc692b", 0, "[]", "[]", "265ae577-9117-45aa-8c52-ad8a8a30ac91", "super_admin@admin.com", true, false, null, "Super Admin", "SUPER_ADMIN@ADMIN.COM", "SUPER_ADMIN", "AQAAAAIAAYagAAAAEFSxIXtcfkLgQAT1TEJF7GNbTC0nychqncB2nlBxLI89ZVFLjncvhE/qfDzL0T0RdA==", "1111111111", true, "a61f1b3e-b05f-4f2a-a7cd-a2dfaaa97d86", 1000000, false, "super_admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dd480c93-5329-45d3-8269-ac2582a70f81", "5d074a2d-7b00-4156-8636-f3a9e6cc692b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25bb5472-ad64-47ca-a605-06f0b78c5687");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41329b3d-4536-40b4-8fc3-50af5d86affe");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dd480c93-5329-45d3-8269-ac2582a70f81", "5d074a2d-7b00-4156-8636-f3a9e6cc692b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd480c93-5329-45d3-8269-ac2582a70f81");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d074a2d-7b00-4156-8636-f3a9e6cc692b");

            migrationBuilder.DropColumn(
                name: "IsAcceptedByUser",
                table: "Agreements");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8614b66d-743f-4a6e-8c9f-7de730680959", null, "Test", "TEST" },
                    { "c00b4910-470e-4117-a006-cbfdf31c07a7", null, "User", "USER" },
                    { "d3011c96-f1fb-40df-b58f-b0b1c1346923", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Books_Borrowed", "Books_Lent", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tokens_Available", "TwoFactorEnabled", "UserName" },
                values: new object[] { "05f0dc92-33e5-4a99-bf6b-44847148f19c", 0, "[]", "[]", "e591a94a-657f-4cc2-abdd-55fed27f37d5", "super_admin@admin.com", true, false, null, "Super Admin", "SUPER_ADMIN@ADMIN.COM", "SUPER_ADMIN", "AQAAAAIAAYagAAAAECXadJi3Abu60nqdQ9VVIGSFOKIW3E/tAPCxwP82QTbWbYAp2L9lRF8P84AyWrhJwA==", "1111111111", true, "a1890f37-3794-430f-8707-af0321920f0a", 1000000, false, "super_admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d3011c96-f1fb-40df-b58f-b0b1c1346923", "05f0dc92-33e5-4a99-bf6b-44847148f19c" });
        }
    }
}
