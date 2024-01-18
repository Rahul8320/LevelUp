﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBorrowingApp.Application.Migrations
{
    /// <inheritdoc />
    public partial class RoleUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
