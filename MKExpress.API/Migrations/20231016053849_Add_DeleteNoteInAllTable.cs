using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class Add_DeleteNoteInAllTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "ShipmentTrackings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "ShipmentImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "ShipmentDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "MasterJourneyDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "MasterJouneys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "MasterDataTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "MasterDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "LogisticRegions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "ContainerTrackings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "ContainerJourneys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleteNote",
                table: "ContainerDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "ShipmentTrackings");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "ShipmentImages");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "ShipmentDetails");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "MasterJourneyDetails");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "MasterJouneys");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "MasterDataTypes");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "MasterDatas");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "LogisticRegions");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "ContainerTrackings");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "DeleteNote",
                table: "ContainerDetails");
        }
    }
}
