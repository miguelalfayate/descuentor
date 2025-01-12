using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Descuentor.Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class MapeoTiendaOnline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "FechaCreacion" },
                values: new object[] { "ebebdb9a-bb55-49a9-8c63-70ae09a7a8ea", new DateTime(2025, 1, 12, 18, 18, 15, 329, DateTimeKind.Utc).AddTicks(6760) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "FechaCreacion" },
                values: new object[] { "f7cb7225-4b6c-46c7-875c-145754d64aba", new DateTime(2025, 1, 12, 18, 18, 15, 329, DateTimeKind.Utc).AddTicks(6840) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "FechaCreacion" },
                values: new object[] { "6240bdce-50ac-41d0-b09b-ac53babf537d", new DateTime(2024, 12, 31, 16, 48, 51, 211, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "FechaCreacion" },
                values: new object[] { "4e696da3-407b-490e-aeb2-2418856146a3", new DateTime(2024, 12, 31, 16, 48, 51, 211, DateTimeKind.Utc).AddTicks(8860) });
        }
    }
}
