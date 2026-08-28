using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoApp1.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusSeedingAndRemoveIsCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "ToDos");

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Colour", "Name" },
                values: new object[,]
                {
                    { 1, "", "Tamamlanmadı" },
                    { 2, "", "Devam Ediyor" },
                    { 3, "", "Tamamlandı" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "ToDos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
