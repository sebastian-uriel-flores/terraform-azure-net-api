using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerraAzSQLAPI.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaID", "Descripcion", "Nombre", "Peso" },
                values: new object[] { new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"), null, "Actividades Pendientes", 20 });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaID", "Descripcion", "Nombre", "Peso" },
                values: new object[] { new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"), null, "Actividades Personales", 50 });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaID", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[] { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfb"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"), null, new DateTime(2022, 9, 10, 10, 16, 58, 184, DateTimeKind.Local).AddTicks(7933), 1, "Pago de servicios públicos" });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaID", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[] { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfc"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"), null, new DateTime(2022, 9, 10, 10, 16, 58, 184, DateTimeKind.Local).AddTicks(7954), 2, "Ir al dentista" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaID",
                keyValue: new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfb"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaID",
                keyValue: new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfc"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaID",
                keyValue: new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
