using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2c0a2d8e-2481-4974-bcb0-747d1ef86896"), "USER", "User", "USER" },
                    { new Guid("d203dd8c-a6fe-4c00-9470-1019d098c00a"), "ADMIN", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("7a0ae26d-76cb-4c51-a9ea-f3b1873813b9"), 0, "fce14cfe-e2c4-49bd-ab1a-c724bf93d6f4", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEBoAogcJTExNy/H/bD4Mq/9JAjPxP72d4Vylfw+RBDeFyt8znLD+98T5ZWt1+F2jjA==", null, false, null, false, "ADMIN" });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0a059ae5-0172-4d30-87cf-20f542c4f7d1"), "Books" },
                    { new Guid("2a986b9c-612b-4b42-92a6-568943879c83"), "Electronics" },
                    { new Guid("60ef0a23-ae06-4241-b51a-73c56ffb2382"), "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d203dd8c-a6fe-4c00-9470-1019d098c00a"), new Guid("7a0ae26d-76cb-4c51-a9ea-f3b1873813b9") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c0a2d8e-2481-4974-bcb0-747d1ef86896"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d203dd8c-a6fe-4c00-9470-1019d098c00a"), new Guid("7a0ae26d-76cb-4c51-a9ea-f3b1873813b9") });

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("0a059ae5-0172-4d30-87cf-20f542c4f7d1"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("2a986b9c-612b-4b42-92a6-568943879c83"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("60ef0a23-ae06-4241-b51a-73c56ffb2382"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d203dd8c-a6fe-4c00-9470-1019d098c00a"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7a0ae26d-76cb-4c51-a9ea-f3b1873813b9"));

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
    }
}
