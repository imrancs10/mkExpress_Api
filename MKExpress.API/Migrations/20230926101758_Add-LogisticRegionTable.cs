using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class AddLogisticRegionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogisticRegions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentStationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticRegions_MasterDatas_CityId",
                        column: x => x.CityId,
                        principalTable: "MasterDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LogisticRegions_MasterDatas_CountryId",
                        column: x => x.CountryId,
                        principalTable: "MasterDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LogisticRegions_MasterDatas_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "MasterDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LogisticRegions_MasterDatas_ParentStationId",
                        column: x => x.ParentStationId,
                        principalTable: "MasterDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LogisticRegions_MasterDatas_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "MasterDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LogisticRegions_MasterDatas_StationId",
                        column: x => x.StationId,
                        principalTable: "MasterDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogisticRegions_CityId",
                table: "LogisticRegions",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticRegions_CountryId",
                table: "LogisticRegions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticRegions_DistrictId",
                table: "LogisticRegions",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticRegions_ParentStationId",
                table: "LogisticRegions",
                column: "ParentStationId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticRegions_ProvinceId",
                table: "LogisticRegions",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticRegions_StationId",
                table: "LogisticRegions",
                column: "StationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogisticRegions");
        }
    }
}
