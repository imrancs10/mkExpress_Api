using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Add_CrystalTrackingOutDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrystalTrackingOuts_MasterCrystals_CrystalId",
                table: "CrystalTrackingOuts");

            migrationBuilder.DropIndex(
                name: "IX_CrystalTrackingOuts_CrystalId",
                table: "CrystalTrackingOuts");

            migrationBuilder.DropColumn(
                name: "CrystalId",
                table: "CrystalTrackingOuts");

            migrationBuilder.DropColumn(
                name: "ReleasePacketQty",
                table: "CrystalTrackingOuts");

            migrationBuilder.DropColumn(
                name: "ReleasePieceQty",
                table: "CrystalTrackingOuts");

            migrationBuilder.DropColumn(
                name: "ReturnPacketQty",
                table: "CrystalTrackingOuts");

            migrationBuilder.DropColumn(
                name: "ReturnPieceQty",
                table: "CrystalTrackingOuts");

            migrationBuilder.CreateTable(
                name: "CrystalTrackingOutDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackingOutId = table.Column<int>(type: "int", nullable: false),
                    CrystalId = table.Column<int>(type: "int", nullable: false),
                    ReleasePacketQty = table.Column<int>(type: "int", nullable: false),
                    ReleasePieceQty = table.Column<int>(type: "int", nullable: false),
                    ReturnPacketQty = table.Column<int>(type: "int", nullable: false),
                    ReturnPieceQty = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalTrackingOutDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalTrackingOutDetail_CrystalTrackingOuts_TrackingOutId",
                        column: x => x.TrackingOutId,
                        principalTable: "CrystalTrackingOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrystalTrackingOutDetail_MasterCrystals_CrystalId",
                        column: x => x.CrystalId,
                        principalTable: "MasterCrystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOutDetail_CrystalId",
                table: "CrystalTrackingOutDetail",
                column: "CrystalId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOutDetail_TrackingOutId",
                table: "CrystalTrackingOutDetail",
                column: "TrackingOutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrystalTrackingOutDetail");

            migrationBuilder.AddColumn<int>(
                name: "CrystalId",
                table: "CrystalTrackingOuts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReleasePacketQty",
                table: "CrystalTrackingOuts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReleasePieceQty",
                table: "CrystalTrackingOuts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReturnPacketQty",
                table: "CrystalTrackingOuts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReturnPieceQty",
                table: "CrystalTrackingOuts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOuts_CrystalId",
                table: "CrystalTrackingOuts",
                column: "CrystalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CrystalTrackingOuts_MasterCrystals_CrystalId",
                table: "CrystalTrackingOuts",
                column: "CrystalId",
                principalTable: "MasterCrystals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
