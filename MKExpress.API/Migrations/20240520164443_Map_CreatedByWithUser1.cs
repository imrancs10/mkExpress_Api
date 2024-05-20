using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class Map_CreatedByWithUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ThirdPartyShipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ThirdPartyShipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ThirdPartyShipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ThirdPartyCourierCompanies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ThirdPartyCourierCompanies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ThirdPartyCourierCompanies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ShipmentTrackings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ShipmentTrackings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ShipmentTrackings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Shipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Shipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Shipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ShipmentImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ShipmentImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ShipmentImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ShipmentDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ShipmentDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ShipmentDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "MasterJourneyDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "MasterJourneyDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "MasterJourneyDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "MasterJouneys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "MasterJouneys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "MasterJouneys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "MasterDataTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "MasterDataTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "MasterDataTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "MasterDatas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "MasterDatas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "MasterDatas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "LogisticRegions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "LogisticRegions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "LogisticRegions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ContainerTrackings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ContainerTrackings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ContainerTrackings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Containers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Containers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Containers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ContainerJourneys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ContainerJourneys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ContainerJourneys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ContainerDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "ContainerDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ContainerDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "AssignShipmentMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "AssignShipmentMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "AssignShipmentMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "AppSettings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "AppSettings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "AppSettings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedBy",
                table: "Users",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyShipments_CreatedBy",
                table: "ThirdPartyShipments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyCourierCompanies_CreatedBy",
                table: "ThirdPartyCourierCompanies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentTrackings_CreatedBy",
                table: "ShipmentTrackings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_CreatedBy",
                table: "Shipments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentImages_CreatedBy",
                table: "ShipmentImages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDetails_CreatedBy",
                table: "ShipmentDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Members_CreatedBy",
                table: "Members",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MasterJourneyDetails_CreatedBy",
                table: "MasterJourneyDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MasterJouneys_CreatedBy",
                table: "MasterJouneys",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDataTypes_CreatedBy",
                table: "MasterDataTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MasterDatas_CreatedBy",
                table: "MasterDatas",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticRegions_CreatedBy",
                table: "LogisticRegions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedBy",
                table: "Customers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerTrackings_CreatedBy",
                table: "ContainerTrackings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_CreatedBy",
                table: "Containers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerJourneys_CreatedBy",
                table: "ContainerJourneys",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerDetails_CreatedBy",
                table: "ContainerDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AssignShipmentMembers_CreatedBy",
                table: "AssignShipmentMembers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AppSettings_CreatedBy",
                table: "AppSettings",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSettings_Users_CreatedBy",
                table: "AppSettings",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignShipmentMembers_Users_CreatedBy",
                table: "AssignShipmentMembers",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerDetails_Users_CreatedBy",
                table: "ContainerDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerJourneys_Users_CreatedBy",
                table: "ContainerJourneys",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Users_CreatedBy",
                table: "Containers",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerTrackings_Users_CreatedBy",
                table: "ContainerTrackings",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_CreatedBy",
                table: "Customers",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogisticRegions_Users_CreatedBy",
                table: "LogisticRegions",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterDatas_Users_CreatedBy",
                table: "MasterDatas",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterDataTypes_Users_CreatedBy",
                table: "MasterDataTypes",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterJouneys_Users_CreatedBy",
                table: "MasterJouneys",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterJourneyDetails_Users_CreatedBy",
                table: "MasterJourneyDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Users_CreatedBy",
                table: "Members",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentDetails_Users_CreatedBy",
                table: "ShipmentDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentImages_Users_CreatedBy",
                table: "ShipmentImages",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Users_CreatedBy",
                table: "Shipments",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentTrackings_Users_CreatedBy",
                table: "ShipmentTrackings",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThirdPartyCourierCompanies_Users_CreatedBy",
                table: "ThirdPartyCourierCompanies",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThirdPartyShipments_Users_CreatedBy",
                table: "ThirdPartyShipments",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CreatedBy",
                table: "Users",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSettings_Users_CreatedBy",
                table: "AppSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignShipmentMembers_Users_CreatedBy",
                table: "AssignShipmentMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerDetails_Users_CreatedBy",
                table: "ContainerDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerJourneys_Users_CreatedBy",
                table: "ContainerJourneys");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Users_CreatedBy",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerTrackings_Users_CreatedBy",
                table: "ContainerTrackings");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_CreatedBy",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_LogisticRegions_Users_CreatedBy",
                table: "LogisticRegions");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterDatas_Users_CreatedBy",
                table: "MasterDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterDataTypes_Users_CreatedBy",
                table: "MasterDataTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterJouneys_Users_CreatedBy",
                table: "MasterJouneys");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterJourneyDetails_Users_CreatedBy",
                table: "MasterJourneyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Users_CreatedBy",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentDetails_Users_CreatedBy",
                table: "ShipmentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentImages_Users_CreatedBy",
                table: "ShipmentImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Users_CreatedBy",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentTrackings_Users_CreatedBy",
                table: "ShipmentTrackings");

            migrationBuilder.DropForeignKey(
                name: "FK_ThirdPartyCourierCompanies_Users_CreatedBy",
                table: "ThirdPartyCourierCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_ThirdPartyShipments_Users_CreatedBy",
                table: "ThirdPartyShipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CreatedBy",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CreatedBy",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ThirdPartyShipments_CreatedBy",
                table: "ThirdPartyShipments");

            migrationBuilder.DropIndex(
                name: "IX_ThirdPartyCourierCompanies_CreatedBy",
                table: "ThirdPartyCourierCompanies");

            migrationBuilder.DropIndex(
                name: "IX_ShipmentTrackings_CreatedBy",
                table: "ShipmentTrackings");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_CreatedBy",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_ShipmentImages_CreatedBy",
                table: "ShipmentImages");

            migrationBuilder.DropIndex(
                name: "IX_ShipmentDetails_CreatedBy",
                table: "ShipmentDetails");

            migrationBuilder.DropIndex(
                name: "IX_Members_CreatedBy",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_MasterJourneyDetails_CreatedBy",
                table: "MasterJourneyDetails");

            migrationBuilder.DropIndex(
                name: "IX_MasterJouneys_CreatedBy",
                table: "MasterJouneys");

            migrationBuilder.DropIndex(
                name: "IX_MasterDataTypes_CreatedBy",
                table: "MasterDataTypes");

            migrationBuilder.DropIndex(
                name: "IX_MasterDatas_CreatedBy",
                table: "MasterDatas");

            migrationBuilder.DropIndex(
                name: "IX_LogisticRegions_CreatedBy",
                table: "LogisticRegions");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CreatedBy",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_ContainerTrackings_CreatedBy",
                table: "ContainerTrackings");

            migrationBuilder.DropIndex(
                name: "IX_Containers_CreatedBy",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_ContainerJourneys_CreatedBy",
                table: "ContainerJourneys");

            migrationBuilder.DropIndex(
                name: "IX_ContainerDetails_CreatedBy",
                table: "ContainerDetails");

            migrationBuilder.DropIndex(
                name: "IX_AssignShipmentMembers_CreatedBy",
                table: "AssignShipmentMembers");

            migrationBuilder.DropIndex(
                name: "IX_AppSettings_CreatedBy",
                table: "AppSettings");

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
    }
}
