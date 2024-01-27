using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class Update_ShimpentImages_FK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentImages_ShipmentTrackings_ShipmentId",
                table: "ShipmentImages");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentImages_TrackingId",
                table: "ShipmentImages",
                column: "TrackingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentImages_ShipmentTrackings_TrackingId",
                table: "ShipmentImages",
                column: "TrackingId",
                principalTable: "ShipmentTrackings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentImages_ShipmentTrackings_TrackingId",
                table: "ShipmentImages");

            migrationBuilder.DropIndex(
                name: "IX_ShipmentImages_TrackingId",
                table: "ShipmentImages");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentImages_ShipmentTrackings_ShipmentId",
                table: "ShipmentImages",
                column: "ShipmentId",
                principalTable: "ShipmentTrackings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
