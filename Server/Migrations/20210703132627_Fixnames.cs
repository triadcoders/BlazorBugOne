using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorBugOne.Server.Migrations
{
    public partial class Fixnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_People_assignedtoid",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_People_personid",
                table: "Bugs");

            migrationBuilder.RenameColumn(
                name: "personid",
                table: "Bugs",
                newName: "personDiscoveredid");

            migrationBuilder.RenameColumn(
                name: "assignedtoid",
                table: "Bugs",
                newName: "personAssignedid");

            migrationBuilder.RenameIndex(
                name: "IX_Bugs_personid",
                table: "Bugs",
                newName: "IX_Bugs_personDiscoveredid");

            migrationBuilder.RenameIndex(
                name: "IX_Bugs_assignedtoid",
                table: "Bugs",
                newName: "IX_Bugs_personAssignedid");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_People_personAssignedid",
                table: "Bugs",
                column: "personAssignedid",
                principalTable: "People",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_People_personDiscoveredid",
                table: "Bugs",
                column: "personDiscoveredid",
                principalTable: "People",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_People_personAssignedid",
                table: "Bugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_People_personDiscoveredid",
                table: "Bugs");

            migrationBuilder.RenameColumn(
                name: "personDiscoveredid",
                table: "Bugs",
                newName: "personid");

            migrationBuilder.RenameColumn(
                name: "personAssignedid",
                table: "Bugs",
                newName: "assignedtoid");

            migrationBuilder.RenameIndex(
                name: "IX_Bugs_personDiscoveredid",
                table: "Bugs",
                newName: "IX_Bugs_personid");

            migrationBuilder.RenameIndex(
                name: "IX_Bugs_personAssignedid",
                table: "Bugs",
                newName: "IX_Bugs_assignedtoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_People_assignedtoid",
                table: "Bugs",
                column: "assignedtoid",
                principalTable: "People",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_People_personid",
                table: "Bugs",
                column: "personid",
                principalTable: "People",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
