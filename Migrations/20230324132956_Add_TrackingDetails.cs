using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Add_TrackingDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrystalTrackingOutDetail_CrystalTrackingOuts_TrackingOutId",
                table: "CrystalTrackingOutDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CrystalTrackingOutDetail_MasterCrystals_CrystalId",
                table: "CrystalTrackingOutDetail");

            migrationBuilder.DropIndex(
                name: "IX_CrystalTrackingOuts_OrderDetailId",
                table: "CrystalTrackingOuts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CrystalTrackingOutDetail",
                table: "CrystalTrackingOutDetail");

            migrationBuilder.RenameTable(
                name: "CrystalTrackingOutDetail",
                newName: "CrystalTrackingOutDetails");

            migrationBuilder.RenameIndex(
                name: "IX_CrystalTrackingOutDetail_TrackingOutId",
                table: "CrystalTrackingOutDetails",
                newName: "IX_CrystalTrackingOutDetails_TrackingOutId");

            migrationBuilder.RenameIndex(
                name: "IX_CrystalTrackingOutDetail_CrystalId",
                table: "CrystalTrackingOutDetails",
                newName: "IX_CrystalTrackingOutDetails_CrystalId");

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "CrystalTrackingOutDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrystalTrackingOutDetails",
                table: "CrystalTrackingOutDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOuts_OrderDetailId",
                table: "CrystalTrackingOuts",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOutDetails_OrderDetailId",
                table: "CrystalTrackingOutDetails",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_CrystalTrackingOutDetails_CrystalTrackingOuts_TrackingOutId",
                table: "CrystalTrackingOutDetails",
                column: "TrackingOutId",
                principalTable: "CrystalTrackingOuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CrystalTrackingOutDetails_MasterCrystals_CrystalId",
                table: "CrystalTrackingOutDetails",
                column: "CrystalId",
                principalTable: "MasterCrystals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CrystalTrackingOutDetails_OrderDetails_OrderDetailId",
                table: "CrystalTrackingOutDetails",
                column: "OrderDetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrystalTrackingOutDetails_CrystalTrackingOuts_TrackingOutId",
                table: "CrystalTrackingOutDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CrystalTrackingOutDetails_MasterCrystals_CrystalId",
                table: "CrystalTrackingOutDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CrystalTrackingOutDetails_OrderDetails_OrderDetailId",
                table: "CrystalTrackingOutDetails");

            migrationBuilder.DropIndex(
                name: "IX_CrystalTrackingOuts_OrderDetailId",
                table: "CrystalTrackingOuts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CrystalTrackingOutDetails",
                table: "CrystalTrackingOutDetails");

            migrationBuilder.DropIndex(
                name: "IX_CrystalTrackingOutDetails_OrderDetailId",
                table: "CrystalTrackingOutDetails");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "CrystalTrackingOutDetails");

            migrationBuilder.RenameTable(
                name: "CrystalTrackingOutDetails",
                newName: "CrystalTrackingOutDetail");

            migrationBuilder.RenameIndex(
                name: "IX_CrystalTrackingOutDetails_TrackingOutId",
                table: "CrystalTrackingOutDetail",
                newName: "IX_CrystalTrackingOutDetail_TrackingOutId");

            migrationBuilder.RenameIndex(
                name: "IX_CrystalTrackingOutDetails_CrystalId",
                table: "CrystalTrackingOutDetail",
                newName: "IX_CrystalTrackingOutDetail_CrystalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrystalTrackingOutDetail",
                table: "CrystalTrackingOutDetail",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOuts_OrderDetailId",
                table: "CrystalTrackingOuts",
                column: "OrderDetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CrystalTrackingOutDetail_CrystalTrackingOuts_TrackingOutId",
                table: "CrystalTrackingOutDetail",
                column: "TrackingOutId",
                principalTable: "CrystalTrackingOuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CrystalTrackingOutDetail_MasterCrystals_CrystalId",
                table: "CrystalTrackingOutDetail",
                column: "CrystalId",
                principalTable: "MasterCrystals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
