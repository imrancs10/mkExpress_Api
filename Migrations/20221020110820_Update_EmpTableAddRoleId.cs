using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_EmpTableAddRoleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserRoleId",
                table: "Employees",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_UserRoles_UserRoleId",
                table: "Employees",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_UserRoles_UserRoleId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserRoleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_UserRoles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "UserRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
