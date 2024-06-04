using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("12ae3c71-3e34-4b38-be6d-8d9d7c4ff80a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("25b98c34-689e-4a94-9dcd-1a725fe4cffd"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("75325abc-b320-49c6-9799-ad93a93c468e"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d7ba4ae9-a32e-4673-bd43-0e8f3f7f1d0e"), "ADMIN", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("0502bd7c-1c26-4430-8d29-f8ec625616e5"), 0, "ce502c1b-0975-4229-9aa3-2e2291af4006", "admin@mail.com", true, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEAwb+L+NZZ+ruLWV0TMzfopMGoOkVHU1mP0J7gg2UHzgtmrpVM1AnKWtHusL5LuL2A==", null, false, null, false, "ADMIN" });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3dd65b6b-d795-495b-b33f-766d5cec8d5a"), "Clothing" },
                    { new Guid("789d7b5c-5c51-4cc1-9743-f04159c6ee85"), "Books" },
                    { new Guid("82abab06-a2c9-4c93-b3fd-4c9611b39c01"), "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d7ba4ae9-a32e-4673-bd43-0e8f3f7f1d0e"), new Guid("0502bd7c-1c26-4430-8d29-f8ec625616e5") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d7ba4ae9-a32e-4673-bd43-0e8f3f7f1d0e"), new Guid("0502bd7c-1c26-4430-8d29-f8ec625616e5") });

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("3dd65b6b-d795-495b-b33f-766d5cec8d5a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("789d7b5c-5c51-4cc1-9743-f04159c6ee85"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("82abab06-a2c9-4c93-b3fd-4c9611b39c01"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d7ba4ae9-a32e-4673-bd43-0e8f3f7f1d0e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0502bd7c-1c26-4430-8d29-f8ec625616e5"));

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("12ae3c71-3e34-4b38-be6d-8d9d7c4ff80a"), "Clothing" },
                    { new Guid("25b98c34-689e-4a94-9dcd-1a725fe4cffd"), "Electronics" },
                    { new Guid("75325abc-b320-49c6-9799-ad93a93c468e"), "Books" }
                });
        }
    }
}
