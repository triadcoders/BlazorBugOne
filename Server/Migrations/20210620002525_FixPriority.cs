using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorBugOne.Server.Migrations
{
    public partial class FixPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Project_projectid",
                table: "Bugs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "priority",
                table: "Bugs",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Projects_projectid",
                table: "Bugs",
                column: "projectid",
                principalTable: "Projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Projects_projectid",
                table: "Bugs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "Bugs");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Project_projectid",
                table: "Bugs",
                column: "projectid",
                principalTable: "Project",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
