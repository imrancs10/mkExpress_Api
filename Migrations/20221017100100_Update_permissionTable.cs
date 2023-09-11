using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_permissionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionResource_ResourceType_ResourceTypeId",
                table: "PermissionResource");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermission_PermissionResource_PermissionResourceId",
                table: "UserPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermission_Users_UserId",
                table: "UserPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermission",
                table: "UserPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceType",
                table: "ResourceType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionResource",
                table: "PermissionResource");

            migrationBuilder.RenameTable(
                name: "UserPermission",
                newName: "UserPermissions");

            migrationBuilder.RenameTable(
                name: "ResourceType",
                newName: "ResourceTypes");

            migrationBuilder.RenameTable(
                name: "PermissionResource",
                newName: "PermissionResources");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermission_UserId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermission_PermissionResourceId",
                table: "UserPermissions",
                newName: "IX_UserPermissions_PermissionResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionResource_ResourceTypeId",
                table: "PermissionResources",
                newName: "IX_PermissionResources_ResourceTypeId");

            migrationBuilder.AddColumn<bool>(
                name: "Create",
                table: "UserPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceTypes",
                table: "ResourceTypes",
                column: "ResourceTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionResources",
                table: "PermissionResources",
                column: "PermissionResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionResources_ResourceTypes_ResourceTypeId",
                table: "PermissionResources",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "ResourceTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_PermissionResources_PermissionResourceId",
                table: "UserPermissions",
                column: "PermissionResourceId",
                principalTable: "PermissionResources",
                principalColumn: "PermissionResourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionResources_ResourceTypes_ResourceTypeId",
                table: "PermissionResources");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_PermissionResources_PermissionResourceId",
                table: "UserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_Users_UserId",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissions",
                table: "UserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceTypes",
                table: "ResourceTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionResources",
                table: "PermissionResources");

            migrationBuilder.DropColumn(
                name: "Create",
                table: "UserPermissions");

            migrationBuilder.RenameTable(
                name: "UserPermissions",
                newName: "UserPermission");

            migrationBuilder.RenameTable(
                name: "ResourceTypes",
                newName: "ResourceType");

            migrationBuilder.RenameTable(
                name: "PermissionResources",
                newName: "PermissionResource");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_UserId",
                table: "UserPermission",
                newName: "IX_UserPermission_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPermissions_PermissionResourceId",
                table: "UserPermission",
                newName: "IX_UserPermission_PermissionResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionResources_ResourceTypeId",
                table: "PermissionResource",
                newName: "IX_PermissionResource_ResourceTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermission",
                table: "UserPermission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceType",
                table: "ResourceType",
                column: "ResourceTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionResource",
                table: "PermissionResource",
                column: "PermissionResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionResource_ResourceType_ResourceTypeId",
                table: "PermissionResource",
                column: "ResourceTypeId",
                principalTable: "ResourceType",
                principalColumn: "ResourceTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_PermissionResource_PermissionResourceId",
                table: "UserPermission",
                column: "PermissionResourceId",
                principalTable: "PermissionResource",
                principalColumn: "PermissionResourceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_Users_UserId",
                table: "UserPermission",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
