using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class AddContainerTrackingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContainerJourneys_Members_CheckInById",
                table: "ContainerJourneys");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerJourneys_Members_CheckOutById",
                table: "ContainerJourneys");

            migrationBuilder.DropForeignKey(
                name: "FK_masterJourneyDetails_MasterDatas_SubStationId",
                table: "masterJourneyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_masterJourneyDetails_MasterJouneys_MasterJourneyId",
                table: "masterJourneyDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_masterJourneyDetails",
                table: "masterJourneyDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContainerJourneys_CheckInById",
                table: "ContainerJourneys");

            migrationBuilder.DropIndex(
                name: "IX_ContainerJourneys_CheckOutById",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "CheckInById",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "CheckOutById",
                table: "ContainerJourneys");

            migrationBuilder.RenameTable(
                name: "masterJourneyDetails",
                newName: "MasterJourneyDetails");

            migrationBuilder.RenameIndex(
                name: "IX_masterJourneyDetails_SubStationId",
                table: "MasterJourneyDetails",
                newName: "IX_MasterJourneyDetails_SubStationId");

            migrationBuilder.RenameIndex(
                name: "IX_masterJourneyDetails_MasterJourneyId",
                table: "MasterJourneyDetails",
                newName: "IX_MasterJourneyDetails_MasterJourneyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterJourneyDetails",
                table: "MasterJourneyDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContainerTrackings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerJourneyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerTrackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerTrackings_ContainerJourneys_ContainerJourneyId",
                        column: x => x.ContainerJourneyId,
                        principalTable: "ContainerJourneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ContainerTrackings_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ContainerTrackings_Members_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerTrackings_ContainerId",
                table: "ContainerTrackings",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerTrackings_ContainerJourneyId",
                table: "ContainerTrackings",
                column: "ContainerJourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerTrackings_CreatedById",
                table: "ContainerTrackings",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterJourneyDetails_MasterDatas_SubStationId",
                table: "MasterJourneyDetails",
                column: "SubStationId",
                principalTable: "MasterDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterJourneyDetails_MasterJouneys_MasterJourneyId",
                table: "MasterJourneyDetails",
                column: "MasterJourneyId",
                principalTable: "MasterJouneys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterJourneyDetails_MasterDatas_SubStationId",
                table: "MasterJourneyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterJourneyDetails_MasterJouneys_MasterJourneyId",
                table: "MasterJourneyDetails");

            migrationBuilder.DropTable(
                name: "ContainerTrackings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterJourneyDetails",
                table: "MasterJourneyDetails");

            migrationBuilder.RenameTable(
                name: "MasterJourneyDetails",
                newName: "masterJourneyDetails");

            migrationBuilder.RenameIndex(
                name: "IX_MasterJourneyDetails_SubStationId",
                table: "masterJourneyDetails",
                newName: "IX_masterJourneyDetails_SubStationId");

            migrationBuilder.RenameIndex(
                name: "IX_MasterJourneyDetails_MasterJourneyId",
                table: "masterJourneyDetails",
                newName: "IX_masterJourneyDetails_MasterJourneyId");

            migrationBuilder.AddColumn<Guid>(
                name: "CheckInById",
                table: "ContainerJourneys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CheckOutById",
                table: "ContainerJourneys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_masterJourneyDetails",
                table: "masterJourneyDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerJourneys_CheckInById",
                table: "ContainerJourneys",
                column: "CheckInById");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerJourneys_CheckOutById",
                table: "ContainerJourneys",
                column: "CheckOutById");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerJourneys_Members_CheckInById",
                table: "ContainerJourneys",
                column: "CheckInById",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerJourneys_Members_CheckOutById",
                table: "ContainerJourneys",
                column: "CheckOutById",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_masterJourneyDetails_MasterDatas_SubStationId",
                table: "masterJourneyDetails",
                column: "SubStationId",
                principalTable: "MasterDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_masterJourneyDetails_MasterJouneys_MasterJourneyId",
                table: "masterJourneyDetails",
                column: "MasterJourneyId",
                principalTable: "MasterJouneys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
