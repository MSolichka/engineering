using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removed_admin_from_mock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { new Guid("c57b7d8b-c4be-4097-b34c-a5a6f5bcc231"), "ADMIN", "Admin", "ADMIN" },
                    { new Guid("dcde34ce-7ef7-40af-a07b-b4c0878b65e0"), "USER", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("05c98ba2-1c6c-4528-a6c2-184e01f02def"), "Books" },
                    { new Guid("3d1b52a8-265b-481e-a2e2-2968e7429df4"), "Clothing" },
                    { new Guid("620abcc2-c39b-4d81-802f-86b586fb556c"), "Electronics" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c57b7d8b-c4be-4097-b34c-a5a6f5bcc231"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcde34ce-7ef7-40af-a07b-b4c0878b65e0"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("05c98ba2-1c6c-4528-a6c2-184e01f02def"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("3d1b52a8-265b-481e-a2e2-2968e7429df4"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("620abcc2-c39b-4d81-802f-86b586fb556c"));

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
    }
}
