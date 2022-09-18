using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerraAzSQLAPI.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tarea",
                keyColumn: "TareaID",
                keyValue: new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfb"),
                column: "FechaCreacion",
                value: new DateTime(2022, 9, 11, 18, 24, 38, 906, DateTimeKind.Local).AddTicks(823));

            migrationBuilder.UpdateData(
                table: "Tarea",
                keyColumn: "TareaID",
                keyValue: new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfc"),
                column: "FechaCreacion",
                value: new DateTime(2022, 9, 11, 18, 24, 38, 906, DateTimeKind.Local).AddTicks(992));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tarea",
                keyColumn: "TareaID",
                keyValue: new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfb"),
                column: "FechaCreacion",
                value: new DateTime(2022, 9, 10, 10, 16, 58, 184, DateTimeKind.Local).AddTicks(7933));

            migrationBuilder.UpdateData(
                table: "Tarea",
                keyColumn: "TareaID",
                keyValue: new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfc"),
                column: "FechaCreacion",
                value: new DateTime(2022, 9, 10, 10, 16, 58, 184, DateTimeKind.Local).AddTicks(7954));
        }
    }
}
