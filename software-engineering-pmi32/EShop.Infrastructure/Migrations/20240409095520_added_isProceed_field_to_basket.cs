using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added_isProceed_field_to_basket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("81bf8c18-55f7-4a89-b327-223fa5a4de87"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9fac1dd3-15d6-45e1-aa48-875a69ae50eb"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("17361bfd-e1a2-40c5-acbd-9009bf453eb5"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("32d23da3-c7b4-4c25-8d52-3d43eccac4a8"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("feb36a5f-95c9-458d-bd05-682be15f509e"));

            migrationBuilder.AddColumn<bool>(
                name: "IsProceed",
                table: "Baskets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5fceeb8a-43a4-48d6-9c7b-d06890196b4a"), "ADMIN", "Admin", "ADMIN" },
                    { new Guid("a8344d31-9af8-43b3-aa69-ca7b2f105ad0"), "USER", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("21d4a646-5db0-4abf-91a1-f3575a59b792"), "Clothing" },
                    { new Guid("57a79874-0e44-4ea1-a134-ef7b902754c3"), "Electronics" },
                    { new Guid("7b3343ba-61cf-4ecd-91a8-a1a3d6b1344d"), "Books" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5fceeb8a-43a4-48d6-9c7b-d06890196b4a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a8344d31-9af8-43b3-aa69-ca7b2f105ad0"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("21d4a646-5db0-4abf-91a1-f3575a59b792"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("57a79874-0e44-4ea1-a134-ef7b902754c3"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("7b3343ba-61cf-4ecd-91a8-a1a3d6b1344d"));

            migrationBuilder.DropColumn(
                name: "IsProceed",
                table: "Baskets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("81bf8c18-55f7-4a89-b327-223fa5a4de87"), "USER", "User", "USER" },
                    { new Guid("9fac1dd3-15d6-45e1-aa48-875a69ae50eb"), "ADMIN", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("17361bfd-e1a2-40c5-acbd-9009bf453eb5"), "Electronics" },
                    { new Guid("32d23da3-c7b4-4c25-8d52-3d43eccac4a8"), "Clothing" },
                    { new Guid("feb36a5f-95c9-458d-bd05-682be15f509e"), "Books" }
                });
        }
    }
}
