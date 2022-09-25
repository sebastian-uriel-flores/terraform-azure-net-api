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
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Job_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Description", "Name", "Weight" },
                values: new object[] { new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"), null, "Things to buy", 20 });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Description", "Name", "Weight" },
                values: new object[] { new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"), null, "Platzi courses to do", 50 });

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "JobId", "CategoryId", "CreationDate", "Description", "Priority", "Title" },
                values: new object[,]
                {
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfa"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"), new DateTime(2022, 9, 24, 21, 45, 14, 613, DateTimeKind.Local).AddTicks(2936), null, 1, "Milk" },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfb"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"), new DateTime(2022, 9, 24, 21, 45, 14, 613, DateTimeKind.Local).AddTicks(2967), null, 2, "Dog food" },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfc"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"), new DateTime(2022, 9, 24, 21, 45, 14, 613, DateTimeKind.Local).AddTicks(2975), null, 2, "Kubernetes" },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfd"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"), new DateTime(2022, 9, 24, 21, 45, 14, 613, DateTimeKind.Local).AddTicks(2984), null, 2, "New Relic" },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfe"), new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"), new DateTime(2022, 9, 24, 21, 45, 14, 613, DateTimeKind.Local).AddTicks(2989), null, 2, "Azure Databases" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_CategoryId",
                table: "Job",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
