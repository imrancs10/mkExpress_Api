using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MKExpress.Web.API.Migrations
{
    public partial class Add_PurchaseEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.CreateTable(
                name: "PurchaseEntries",
                columns: table => new
                {
                    PurchaseEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseNo = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TRN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseEntries", x => x.PurchaseEntryId);
                    table.ForeignKey(
                        name: "FK_PurchaseEntries_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseEntryDetails",
                columns: table => new
                {
                    PurchaseEntryDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseEntryId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    FabricWidthId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseEntryDetails", x => x.PurchaseEntryDetailId);
                    table.ForeignKey(
                        name: "FK_PurchaseEntryDetails_MasterDatas_BrandId",
                        column: x => x.BrandId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PurchaseEntryDetails_MasterDatas_FabricWidthId",
                        column: x => x.FabricWidthId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PurchaseEntryDetails_MasterDatas_ItemId",
                        column: x => x.ItemId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PurchaseEntryDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseEntryDetails_PurchaseEntries_PurchaseEntryId",
                        column: x => x.PurchaseEntryId,
                        principalTable: "PurchaseEntries",
                        principalColumn: "PurchaseEntryId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntries_SupplierId",
                table: "PurchaseEntries",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntryDetails_BrandId",
                table: "PurchaseEntryDetails",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntryDetails_FabricWidthId",
                table: "PurchaseEntryDetails",
                column: "FabricWidthId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntryDetails_ItemId",
                table: "PurchaseEntryDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntryDetails_ProductId",
                table: "PurchaseEntryDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntryDetails_PurchaseEntryId",
                table: "PurchaseEntryDetails",
                column: "PurchaseEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseEntryDetails");

            migrationBuilder.DropTable(
                name: "PurchaseEntries");

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand_Buffer = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Brand_Id = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Brand_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand_Pak_Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });
        }
    }
}
