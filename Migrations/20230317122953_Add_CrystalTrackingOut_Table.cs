using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Add_CrystalTrackingOut_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrystalTrackingOuts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CrystalId = table.Column<int>(type: "int", nullable: false),
                    ReleasePacketQty = table.Column<int>(type: "int", nullable: false),
                    ReleasePieceQty = table.Column<int>(type: "int", nullable: false),
                    ReturnPacketQty = table.Column<int>(type: "int", nullable: false),
                    ReturnPieceQty = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalTrackingOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalTrackingOuts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CrystalTrackingOuts_MasterCrystals_CrystalId",
                        column: x => x.CrystalId,
                        principalTable: "MasterCrystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CrystalTrackingOuts_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOuts_CrystalId",
                table: "CrystalTrackingOuts",
                column: "CrystalId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOuts_EmployeeId",
                table: "CrystalTrackingOuts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOuts_OrderDetailId",
                table: "CrystalTrackingOuts",
                column: "OrderDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrystalTrackingOuts");
        }
    }
}
