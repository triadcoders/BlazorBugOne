using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorBugOne.Server.Migrations
{
    public partial class lifecycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lifecycle",
                table: "Bugs",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lifecycle",
                table: "Bugs");
        }
    }
}
