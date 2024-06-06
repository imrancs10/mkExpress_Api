using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShipmentTrackingtableforCommentby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentTrackings_Members_CommentBy",
                table: "ShipmentTrackings");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentTrackings_Users_CommentBy",
                table: "ShipmentTrackings",
                column: "CommentBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentTrackings_Users_CommentBy",
                table: "ShipmentTrackings");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentTrackings_Members_CommentBy",
                table: "ShipmentTrackings",
                column: "CommentBy",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
