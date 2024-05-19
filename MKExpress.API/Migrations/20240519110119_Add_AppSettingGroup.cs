using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class Add_AppSettingGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignShipmentMembers_Members_AssignById",
                table: "AssignShipmentMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignShipmentMembers_Members_MemberId",
                table: "AssignShipmentMembers");

            migrationBuilder.RenameColumn(
                name: "Group",
                table: "AppSettings",
                newName: "GroupId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PasswordResetCodeExpireOn",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "AssignShipmentMembers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignById",
                table: "AssignShipmentMembers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignAt",
                table: "AssignShipmentMembers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "AppSettingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettingGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSettings_GroupId",
                table: "AppSettings",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSettings_AppSettingGroups_GroupId",
                table: "AppSettings",
                column: "GroupId",
                principalTable: "AppSettingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignShipmentMembers_Members_AssignById",
                table: "AssignShipmentMembers",
                column: "AssignById",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignShipmentMembers_Members_MemberId",
                table: "AssignShipmentMembers",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSettings_AppSettingGroups_GroupId",
                table: "AppSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignShipmentMembers_Members_AssignById",
                table: "AssignShipmentMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignShipmentMembers_Members_MemberId",
                table: "AssignShipmentMembers");

            migrationBuilder.DropTable(
                name: "AppSettingGroups");

            migrationBuilder.DropIndex(
                name: "IX_AppSettings_GroupId",
                table: "AppSettings");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "AppSettings",
                newName: "Group");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PasswordResetCodeExpireOn",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "AssignShipmentMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignById",
                table: "AssignShipmentMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignAt",
                table: "AssignShipmentMembers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignShipmentMembers_Members_AssignById",
                table: "AssignShipmentMembers",
                column: "AssignById",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignShipmentMembers_Members_MemberId",
                table: "AssignShipmentMembers",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
