using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_TrackingOutAndMasterCrystalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "CrystalTrackingOuts");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "CrystalTrackingOutDetails");

            migrationBuilder.DropColumn(
                name: "ReturnPacketQty",
                table: "CrystalTrackingOutDetails");

            migrationBuilder.RenameColumn(
                name: "ReturnPieceQty",
                table: "CrystalTrackingOutDetails",
                newName: "LoosePieces");

            migrationBuilder.AddColumn<bool>(
                name: "IsArtical",
                table: "MasterCrystals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReleasePacketQty",
                table: "CrystalTrackingOutDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "ArticalLabourCharge",
                table: "CrystalTrackingOutDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CrystalLabourCharge",
                table: "CrystalTrackingOutDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArtical",
                table: "MasterCrystals");

            migrationBuilder.DropColumn(
                name: "ArticalLabourCharge",
                table: "CrystalTrackingOutDetails");

            migrationBuilder.DropColumn(
                name: "CrystalLabourCharge",
                table: "CrystalTrackingOutDetails");

            migrationBuilder.RenameColumn(
                name: "LoosePieces",
                table: "CrystalTrackingOutDetails",
                newName: "ReturnPieceQty");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "CrystalTrackingOuts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "ReleasePacketQty",
                table: "CrystalTrackingOutDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "CrystalTrackingOutDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ReturnPacketQty",
                table: "CrystalTrackingOutDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
