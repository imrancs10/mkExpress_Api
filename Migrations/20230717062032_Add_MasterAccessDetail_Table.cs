using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Add_MasterAccessDetail_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterAccesses_MasterMenus_MasterMenuId",
                table: "MasterAccesses");

            migrationBuilder.DropIndex(
                name: "IX_MasterAccesses_MasterMenuId",
                table: "MasterAccesses");

            migrationBuilder.DropColumn(
                name: "MasterMenuId",
                table: "MasterAccesses");

            migrationBuilder.CreateTable(
                name: "MasterAccessDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessId = table.Column<int>(type: "int", nullable: false),
                    MasterMenuId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterAccessDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterAccessDetail_MasterAccesses_AccessId",
                        column: x => x.AccessId,
                        principalTable: "MasterAccesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MasterAccessDetail_MasterMenus_MasterMenuId",
                        column: x => x.MasterMenuId,
                        principalTable: "MasterMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterAccessDetail_AccessId",
                table: "MasterAccessDetail",
                column: "AccessId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterAccessDetail_MasterMenuId",
                table: "MasterAccessDetail",
                column: "MasterMenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterAccessDetail");

            migrationBuilder.AddColumn<int>(
                name: "MasterMenuId",
                table: "MasterAccesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MasterAccesses_MasterMenuId",
                table: "MasterAccesses",
                column: "MasterMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterAccesses_MasterMenus_MasterMenuId",
                table: "MasterAccesses",
                column: "MasterMenuId",
                principalTable: "MasterMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
