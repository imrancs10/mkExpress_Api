using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class Add_Master_Journey_Table1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterJouneys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromStationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToStationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_MasterJouneys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterJouneys_MasterDatas_FromStationId",
                        column: x => x.FromStationId,
                        principalTable: "MasterDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MasterJouneys_MasterDatas_ToStationId",
                        column: x => x.ToStationId,
                        principalTable: "MasterDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "masterJourneyDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterJourneyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubStationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_masterJourneyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_masterJourneyDetails_MasterDatas_SubStationId",
                        column: x => x.SubStationId,
                        principalTable: "MasterDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_masterJourneyDetails_MasterJouneys_MasterJourneyId",
                        column: x => x.MasterJourneyId,
                        principalTable: "MasterJouneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterJouneys_FromStationId",
                table: "MasterJouneys",
                column: "FromStationId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterJouneys_ToStationId",
                table: "MasterJouneys",
                column: "ToStationId");

            migrationBuilder.CreateIndex(
                name: "IX_masterJourneyDetails_MasterJourneyId",
                table: "masterJourneyDetails",
                column: "MasterJourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_masterJourneyDetails_SubStationId",
                table: "masterJourneyDetails",
                column: "SubStationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "masterJourneyDetails");

            migrationBuilder.DropTable(
                name: "MasterJouneys");
        }
    }
}
