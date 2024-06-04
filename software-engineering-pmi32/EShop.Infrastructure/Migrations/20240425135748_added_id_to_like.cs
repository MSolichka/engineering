using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added_id_to_like : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8fe91815-3da3-4576-9c40-db4864bedcb6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ee4126fc-2cda-4454-aeac-c1f7bb68a230"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("1984cbd7-6543-48fd-88bb-a73b44aa00df"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("78d86a5a-b6ff-41b7-9459-231ac2381919"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("84c3deaf-8cb5-4b55-8494-1fecac4f4c4a"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProductLikes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3596a3f2-3e53-4857-a2d8-b39fa148c7d2"), "USER", "User", "USER" },
                    { new Guid("a5e08d81-94ac-46dc-b27a-5e48f495e62c"), "ADMIN", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1cc9e528-9349-40d2-b646-afa6c526622a"), "Clothing" },
                    { new Guid("45fc4a83-b149-4db1-9706-fb3bcc9b9d2c"), "Electronics" },
                    { new Guid("a6423dc8-4465-4afd-b00a-577ff7e6b816"), "Books" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3596a3f2-3e53-4857-a2d8-b39fa148c7d2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a5e08d81-94ac-46dc-b27a-5e48f495e62c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("1cc9e528-9349-40d2-b646-afa6c526622a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("45fc4a83-b149-4db1-9706-fb3bcc9b9d2c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a6423dc8-4465-4afd-b00a-577ff7e6b816"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductLikes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("8fe91815-3da3-4576-9c40-db4864bedcb6"), "USER", "User", "USER" },
                    { new Guid("ee4126fc-2cda-4454-aeac-c1f7bb68a230"), "ADMIN", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1984cbd7-6543-48fd-88bb-a73b44aa00df"), "Electronics" },
                    { new Guid("78d86a5a-b6ff-41b7-9459-231ac2381919"), "Books" },
                    { new Guid("84c3deaf-8cb5-4b55-8494-1fecac4f4c4a"), "Clothing" }
                });
        }
    }
}
