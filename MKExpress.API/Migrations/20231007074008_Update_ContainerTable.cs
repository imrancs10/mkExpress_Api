using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class Update_ContainerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContainerJourneys_MasterDatas_FromStationId",
                table: "ContainerJourneys");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerJourneys_MasterDatas_ToStationId",
                table: "ContainerJourneys");

            migrationBuilder.DropTable(
                name: "ContainerTrackings");

            migrationBuilder.DropIndex(
                name: "IX_ContainerJourneys_FromStationId",
                table: "ContainerJourneys");

            migrationBuilder.DropIndex(
                name: "IX_ContainerJourneys_ToStationId",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "ContainerType",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "TotalShipments",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "FromStationId",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "ToStationId",
                table: "ContainerJourneys");

            migrationBuilder.AddColumn<Guid>(
                name: "ContainerTypeId",
                table: "Containers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "JourneyId",
                table: "Containers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalAt",
                table: "ContainerJourneys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureOn",
                table: "ContainerJourneys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SequenceNo",
                table: "ContainerJourneys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StationName",
                table: "ContainerJourneys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ContainerTypeId",
                table: "Containers",
                column: "ContainerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_JourneyId",
                table: "Containers",
                column: "JourneyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_MasterDatas_ContainerTypeId",
                table: "Containers",
                column: "ContainerTypeId",
                principalTable: "MasterDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_MasterJouneys_JourneyId",
                table: "Containers",
                column: "JourneyId",
                principalTable: "MasterJouneys",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_MasterDatas_ContainerTypeId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_MasterJouneys_JourneyId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_ContainerTypeId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_JourneyId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "ContainerTypeId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "JourneyId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "ArrivalAt",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "DepartureOn",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "SequenceNo",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "StationName",
                table: "ContainerJourneys");

            migrationBuilder.AddColumn<string>(
                name: "ContainerType",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalShipments",
                table: "Containers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "FromStationId",
                table: "ContainerJourneys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ToStationId",
                table: "ContainerJourneys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ContainerTrackings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerTrackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerTrackings_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContainerTrackings_MasterDatas_StationId",
                        column: x => x.StationId,
                        principalTable: "MasterDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContainerTrackings_Members_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerJourneys_FromStationId",
                table: "ContainerJourneys",
                column: "FromStationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerJourneys_ToStationId",
                table: "ContainerJourneys",
                column: "ToStationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerTrackings_ContainerId",
                table: "ContainerTrackings",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerTrackings_CreatedBy",
                table: "ContainerTrackings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerTrackings_StationId",
                table: "ContainerTrackings",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerJourneys_MasterDatas_FromStationId",
                table: "ContainerJourneys",
                column: "FromStationId",
                principalTable: "MasterDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerJourneys_MasterDatas_ToStationId",
                table: "ContainerJourneys",
                column: "ToStationId",
                principalTable: "MasterDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
