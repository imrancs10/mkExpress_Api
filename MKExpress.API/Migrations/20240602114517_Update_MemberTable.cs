using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class Update_MemberTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MasterDatas_RoleId",
                table: "Members");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_UserRoles_RoleId",
                table: "Members",
                column: "RoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_UserRoles_RoleId",
                table: "Members");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MasterDatas_RoleId",
                table: "Members",
                column: "RoleId",
                principalTable: "MasterDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
