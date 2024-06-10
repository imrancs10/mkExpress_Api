using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class Update_ShipmentAddCodDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CODDate",
                table: "Shipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiveDate",
                table: "Shipments",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CODDate",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ReceiveDate",
                table: "Shipments");
        }
    }
}
