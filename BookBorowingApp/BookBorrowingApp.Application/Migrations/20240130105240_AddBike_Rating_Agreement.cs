using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBorrowingApp.Application.Migrations
{
    /// <inheritdoc />
    public partial class AddBike_Rating_Agreement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58b1e711-1efd-463c-b2b7-9a500139189e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2054dd1-b6f6-4892-971c-b46a36018b34");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b448a858-bfb2-469a-828d-c5de1a24825e", "45b0cea5-a128-4e90-967f-13f9b41153b3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b448a858-bfb2-469a-828d-c5de1a24825e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45b0cea5-a128-4e90-967f-13f9b41153b3");

            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BikeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BikeOwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalCost = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BikeRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BikeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Review = table.Column<string>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeRatings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Owner = table.Column<Guid>(type: "TEXT", nullable: false),
                    Maker = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    RentalPricePerDay = table.Column<int>(type: "INTEGER", nullable: false),
                    LateFeesPerDay = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAvailableForRent = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRequestForReturn = table.Column<bool>(type: "INTEGER", nullable: false),
                    CurrentBikeStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    CoverImage = table.Column<string>(type: "TEXT", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    FuelCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    FuelEconomy = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bikes", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.DropTable(
                name: "BikeRatings");

            migrationBuilder.DropTable(
                name: "Bikes");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "58b1e711-1efd-463c-b2b7-9a500139189e", null, "User", "USER" },
                    { "b448a858-bfb2-469a-828d-c5de1a24825e", null, "Admin", "ADMIN" },
                    { "e2054dd1-b6f6-4892-971c-b46a36018b34", null, "Test", "TEST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Books_Borrowed", "Books_Lent", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Tokens_Available", "TwoFactorEnabled", "UserName" },
                values: new object[] { "45b0cea5-a128-4e90-967f-13f9b41153b3", 0, "[]", "[]", "7ae60470-b997-4d5b-b507-5c49b02b338b", "super_admin@admin.com", true, false, null, "Super Admin", "SUPER_ADMIN@ADMIN.COM", "SUPER_ADMIN", "AQAAAAIAAYagAAAAEE0VTWxFj40sGy0sik1lMuwkwvTN1d/3tS6q6Fu33+f9htBF1UAlXrUzYubGLm5phQ==", "1111111111", true, "885f32a7-23bb-4134-b935-1954cbbc5bed", 1000000, false, "super_admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b448a858-bfb2-469a-828d-c5de1a24825e", "45b0cea5-a128-4e90-967f-13f9b41153b3" });
        }
    }
}
