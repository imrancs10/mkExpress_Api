using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Add_MasterCrystal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterCrystals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlertQty = table.Column<int>(type: "int", nullable: false,defaultValue:50),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    ShapeId = table.Column<int>(type: "int", nullable: false),
                    QtyPerPacket = table.Column<int>(type: "int", nullable: false,defaultValue:1440),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false,defaultValue:false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCrystals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterCrystals_MasterDatas_BrandId",
                        column: x => x.BrandId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MasterCrystals_MasterDatas_ShapeId",
                        column: x => x.ShapeId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MasterCrystals_MasterDatas_SizeId",
                        column: x => x.SizeId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterCrystals_BrandId",
                table: "MasterCrystals",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCrystals_ShapeId",
                table: "MasterCrystals",
                column: "ShapeId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCrystals_SizeId",
                table: "MasterCrystals",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterCrystals");
        }
    }
}
