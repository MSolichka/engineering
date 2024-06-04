using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ProductLikes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLikes", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLikes_Items_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductLikes_ProductId",
                table: "ProductLikes",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductLikes");

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
    }
}
