using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class Update_ShipmentImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "ShipmentImages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ThumbnailUrl",
                table: "ShipmentImages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "ShipmentImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModuleName",
                table: "ShipmentImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "ShipmentImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SequenceNo",
                table: "ShipmentImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrackingId",
                table: "ShipmentImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentImages_ShipmentTrackings_ShipmentId",
                table: "ShipmentImages",
                column: "ShipmentId",
                principalTable: "ShipmentTrackings",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentImages_ShipmentTrackings_ShipmentId",
                table: "ShipmentImages");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "ShipmentImages");

            migrationBuilder.DropColumn(
                name: "ModuleName",
                table: "ShipmentImages");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "ShipmentImages");

            migrationBuilder.DropColumn(
                name: "SequenceNo",
                table: "ShipmentImages");

            migrationBuilder.DropColumn(
                name: "TrackingId",
                table: "ShipmentImages");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "ShipmentImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ThumbnailUrl",
                table: "ShipmentImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
