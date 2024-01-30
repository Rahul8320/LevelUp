using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBorrowingApp.Application.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56aa059f-3819-42ce-a4c5-b57f50b880d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7416d42-9dff-4907-96ac-272337679783");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5b1b0f2-743c-4b9e-9836-249651da4bda");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "56aa059f-3819-42ce-a4c5-b57f50b880d0", "3", "Test", "TEST" },
                    { "d7416d42-9dff-4907-96ac-272337679783", "2", "User", "USER" },
                    { "e5b1b0f2-743c-4b9e-9836-249651da4bda", "1", "Admin", "ADMIN" }
                });
        }
    }
}
