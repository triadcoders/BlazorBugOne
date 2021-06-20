using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorBugOne.Server.Migrations
{
    public partial class AddedAssignee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "assignedtoid",
                table: "Bugs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_assignedtoid",
                table: "Bugs",
                column: "assignedtoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_People_assignedtoid",
                table: "Bugs",
                column: "assignedtoid",
                principalTable: "People",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_People_assignedtoid",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_assignedtoid",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "assignedtoid",
                table: "Bugs");
        }
    }
}
