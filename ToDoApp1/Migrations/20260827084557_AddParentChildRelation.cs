using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp1.Migrations
{
    /// <inheritdoc />
    public partial class AddParentChildRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ToDos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_ParentId",
                table: "ToDos",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_ToDos_ParentId",
                table: "ToDos",
                column: "ParentId",
                principalTable: "ToDos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_ToDos_ParentId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_ParentId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ToDos");
        }
    }
}
