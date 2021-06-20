using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorBugOne.Server.Migrations
{
    public partial class AddedPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_Person_personid",
                table: "Bugs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "People");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_People_personid",
                table: "Bugs",
                column: "personid",
                principalTable: "People",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_People_personid",
                table: "Bugs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "Person");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Person_personid",
                table: "Bugs",
                column: "personid",
                principalTable: "Person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
