using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Add_MasterMenu_Table2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterAccesses_MasterMenus_MenuId",
                table: "MasterAccesses");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "MasterAccesses",
                newName: "MasterMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_MasterAccesses_MenuId",
                table: "MasterAccesses",
                newName: "IX_MasterAccesses_MasterMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterAccesses_MasterMenus_MasterMenuId",
                table: "MasterAccesses",
                column: "MasterMenuId",
                principalTable: "MasterMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterAccesses_MasterMenus_MasterMenuId",
                table: "MasterAccesses");

            migrationBuilder.RenameColumn(
                name: "MasterMenuId",
                table: "MasterAccesses",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_MasterAccesses_MasterMenuId",
                table: "MasterAccesses",
                newName: "IX_MasterAccesses_MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterAccesses_MasterMenus_MenuId",
                table: "MasterAccesses",
                column: "MenuId",
                principalTable: "MasterMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
