using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Descuentor.Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class AddCampoDescripcionEnProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Productos",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "FechaCreacion", "NormalizedUserName", "UserName" },
                values: new object[] { "6240bdce-50ac-41d0-b09b-ac53babf537d", new DateTime(2024, 12, 31, 16, 48, 51, 211, DateTimeKind.Utc).AddTicks(8790), "ADMIN@DOMAIN.COM", "admin@domain.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "FechaCreacion", "NormalizedUserName", "UserName" },
                values: new object[] { "4e696da3-407b-490e-aeb2-2418856146a3", new DateTime(2024, 12, 31, 16, 48, 51, 211, DateTimeKind.Utc).AddTicks(8860), "USER1@DOMAIN.COM", "user1@domain.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Productos");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "FechaCreacion", "NormalizedUserName", "UserName" },
                values: new object[] { "b5700130-81fe-4379-b450-129dfd51c209", new DateTime(2024, 12, 31, 10, 47, 6, 324, DateTimeKind.Utc).AddTicks(8720), "ADMIN", "admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "FechaCreacion", "NormalizedUserName", "UserName" },
                values: new object[] { "0a4dcd24-b4ba-48f1-9cf7-6036cca69e6a", new DateTime(2024, 12, 31, 10, 47, 6, 324, DateTimeKind.Utc).AddTicks(8790), "USER1", "user1" });
        }
    }
}
