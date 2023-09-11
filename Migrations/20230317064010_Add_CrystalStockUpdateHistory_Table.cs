using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Add_CrystalStockUpdateHistory_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrystalStockUpdateHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrystalId = table.Column<int>(type: "int", nullable: false),
                    OldPieces = table.Column<int>(type: "int", nullable: false),
                    OldPackets = table.Column<int>(type: "int", nullable: false),
                    NewPieces = table.Column<int>(type: "int", nullable: false),
                    NewPackets = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalStockUpdateHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalStockUpdateHistories_MasterCrystals_CrystalId",
                        column: x => x.CrystalId,
                        principalTable: "MasterCrystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrystalStockUpdateHistories_CrystalId",
                table: "CrystalStockUpdateHistories",
                column: "CrystalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrystalStockUpdateHistories");
        }
    }
}
