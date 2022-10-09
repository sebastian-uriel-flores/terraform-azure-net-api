using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoAPIAzure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ToDos",
                columns: table => new
                {
                    ToDoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDos", x => x.ToDoId);
                    table.ForeignKey(
                        name: "FK_ToDos_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "Weight" },
                values: new object[] { new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"), null, "Things to buy", 20 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "Weight" },
                values: new object[] { new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"), null, "Platzi courses to do", 50 });

            migrationBuilder.InsertData(
                table: "ToDos",
                columns: new[] { "ToDoId", "CategoryId", "CreationDate", "Description", "Priority", "Title" },
                values: new object[,]
                {
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfa"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"), new DateTime(2022, 10, 9, 11, 27, 18, 324, DateTimeKind.Local).AddTicks(3927), null, 2, "Milk" },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfb"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"), new DateTime(2022, 10, 9, 11, 27, 18, 324, DateTimeKind.Local).AddTicks(3938), null, 3, "Dog food" },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfc"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"), new DateTime(2022, 10, 9, 11, 27, 18, 324, DateTimeKind.Local).AddTicks(3939), null, 3, "Kubernetes" },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfd"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"), new DateTime(2022, 10, 9, 11, 27, 18, 324, DateTimeKind.Local).AddTicks(3940), null, 3, "New Relic" },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfe"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"), new DateTime(2022, 10, 9, 11, 27, 18, 324, DateTimeKind.Local).AddTicks(3941), null, 3, "Azure Databases" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_CategoryId",
                table: "ToDos",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDos");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
