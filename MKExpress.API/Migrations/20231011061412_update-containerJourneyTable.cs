using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class updatecontainerJourneyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShipmentDetails_ShipmentId",
                table: "ShipmentDetails");

            migrationBuilder.DropColumn(
                name: "StationName",
                table: "ContainerJourneys");

            migrationBuilder.AddColumn<bool>(
                name: "IsDestinationStation",
                table: "ContainerJourneys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSourceStation",
                table: "ContainerJourneys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StationId",
                table: "ContainerJourneys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDetails_ShipmentId",
                table: "ShipmentDetails",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerJourneys_StationId",
                table: "ContainerJourneys",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerJourneys_MasterDatas_StationId",
                table: "ContainerJourneys",
                column: "StationId",
                principalTable: "MasterDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContainerJourneys_MasterDatas_StationId",
                table: "ContainerJourneys");

            migrationBuilder.DropIndex(
                name: "IX_ShipmentDetails_ShipmentId",
                table: "ShipmentDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContainerJourneys_StationId",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "IsDestinationStation",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "IsSourceStation",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "ContainerJourneys");

            migrationBuilder.AddColumn<string>(
                name: "StationName",
                table: "ContainerJourneys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDetails_ShipmentId",
                table: "ShipmentDetails",
                column: "ShipmentId");
        }
    }
}
