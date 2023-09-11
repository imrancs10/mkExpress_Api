using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Add_MasterMenu_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "MasterAccesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MasterMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Disable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterMenus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterAccesses_MenuId",
                table: "MasterAccesses",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterAccesses_MasterMenus_MenuId",
                table: "MasterAccesses",
                column: "MenuId",
                principalTable: "MasterMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterAccesses_MasterMenus_MenuId",
                table: "MasterAccesses");

            migrationBuilder.DropTable(
                name: "MasterMenus");

            migrationBuilder.DropIndex(
                name: "IX_MasterAccesses_MenuId",
                table: "MasterAccesses");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "MasterAccesses");
        }
    }
}
