using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Descuentor.Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class CambioNombrePrecioInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrecioActual",
                table: "Productos",
                newName: "PrecioInicial");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "FechaCreacion" },
                values: new object[] { "c0f526f5-c692-423a-8084-84327082b995", new DateTime(2025, 1, 12, 21, 55, 20, 225, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "FechaCreacion" },
                values: new object[] { "4d79018e-0bf6-495a-b5c9-c34b4897997e", new DateTime(2025, 1, 12, 21, 55, 20, 225, DateTimeKind.Utc).AddTicks(4410) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrecioInicial",
                table: "Productos",
                newName: "PrecioActual");

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
    }
}
