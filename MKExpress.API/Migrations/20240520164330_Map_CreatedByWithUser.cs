using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class Map_CreatedByWithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ThirdPartyShipments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ThirdPartyShipments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ThirdPartyShipments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ThirdPartyCourierCompanies");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ThirdPartyCourierCompanies");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ThirdPartyCourierCompanies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ShipmentTrackings");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ShipmentTrackings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ShipmentTrackings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ShipmentImages");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ShipmentImages");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ShipmentImages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ShipmentDetails");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ShipmentDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ShipmentDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MasterJourneyDetails");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "MasterJourneyDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MasterJourneyDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MasterJouneys");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "MasterJouneys");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MasterJouneys");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MasterDataTypes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "MasterDataTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MasterDataTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MasterDatas");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "MasterDatas");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MasterDatas");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LogisticRegions");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LogisticRegions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LogisticRegions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ContainerTrackings");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ContainerTrackings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ContainerTrackings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ContainerDetails");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ContainerDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ContainerDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AssignShipmentMembers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AssignShipmentMembers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AssignShipmentMembers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AppSettings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ThirdPartyShipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "ThirdPartyShipments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "ThirdPartyShipments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ThirdPartyCourierCompanies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "ThirdPartyCourierCompanies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "ThirdPartyCourierCompanies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ShipmentTrackings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "ShipmentTrackings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "ShipmentTrackings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Shipments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Shipments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ShipmentImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "ShipmentImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "ShipmentImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ShipmentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "ShipmentDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "ShipmentDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "MasterJourneyDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "MasterJourneyDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "MasterJourneyDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "MasterJouneys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "MasterJouneys",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "MasterJouneys",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "MasterDataTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "MasterDataTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "MasterDataTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "MasterDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "MasterDatas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "MasterDatas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "LogisticRegions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "LogisticRegions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "LogisticRegions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ContainerTrackings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "ContainerTrackings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "ContainerTrackings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Containers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Containers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Containers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ContainerJourneys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "ContainerJourneys",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "ContainerJourneys",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ContainerDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "ContainerDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "ContainerDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "AssignShipmentMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "AssignShipmentMembers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "AssignShipmentMembers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "AppSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "AppSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "AppSettings",
                type: "int",
                nullable: true);
        }
    }
}
