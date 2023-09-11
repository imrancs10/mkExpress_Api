using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_MasterAccess_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "MasterAccesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MasterAccesses_RoleId",
                table: "MasterAccesses",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterAccesses_UserRoles_RoleId",
                table: "MasterAccesses",
                column: "RoleId",
                principalTable: "UserRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterAccesses_UserRoles_RoleId",
                table: "MasterAccesses");

            migrationBuilder.DropIndex(
                name: "IX_MasterAccesses_RoleId",
                table: "MasterAccesses");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "MasterAccesses");
        }
    }
}
