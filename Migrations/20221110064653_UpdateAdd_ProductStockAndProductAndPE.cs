using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MKExpress.Web.API.Migrations
{
    public partial class UpdateAdd_ProductStockAndProductAndPE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseEntryDetails_MasterDatas_FabricWidthId",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseEntryDetails_MasterDatas_ItemId",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseEntryDetails_FabricWidthId",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseEntryDetails_ItemId",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropColumn(
                name: "FabricWidthId",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "PurchaseEntries");

            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QuantityUnit",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TrnNo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Products",
                newName: "WidthId");

            migrationBuilder.DropColumn(
                name: "PurchaseNo",
                table: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                newName: "IX_Products_WidthId");

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Products",
                type: "int",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AddColumn<int>(
               name: "SizeId",
               table: "Products",
               type: "int",
               nullable: true,
               defaultValue: null);

            migrationBuilder.CreateTable(
                name: "OrderUsedCrystals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UsedQty = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderUsedCrystals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderUsedCrystals_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderUsedCrystals_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TotalQty = table.Column<int>(type: "int", nullable: false),
                    UsedQty = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductStocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeId",
                table: "Products",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsedCrystals_OrderDetailId",
                table: "OrderUsedCrystals",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsedCrystals_ProductId",
                table: "OrderUsedCrystals",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_ProductId",
                table: "ProductStocks",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MasterDatas_SizeId",
                table: "Products",
                column: "SizeId",
                principalTable: "MasterDatas",
                principalColumn: "MasterDataId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MasterDatas_WidthId",
                table: "Products",
                column: "WidthId",
                principalTable: "MasterDatas",
                principalColumn: "MasterDataId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_MasterDatas_SizeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_MasterDatas_WidthId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "OrderUsedCrystals");

            migrationBuilder.DropTable(
                name: "ProductStocks");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SizeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "WidthId",
                table: "Products",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "SizeId",
                table: "Products",
                newName: "PurchaseNo");

            migrationBuilder.RenameIndex(
                name: "IX_Products_WidthId",
                table: "Products",
                newName: "IX_Products_SupplierId");

            migrationBuilder.AddColumn<int>(
                name: "FabricWidthId",
                table: "PurchaseEntryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "PurchaseEntryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "PurchaseEntryDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "PurchaseEntries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "QuantityUnit",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrnNo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntryDetails_FabricWidthId",
                table: "PurchaseEntryDetails",
                column: "FabricWidthId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntryDetails_ItemId",
                table: "PurchaseEntryDetails",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseEntryDetails_MasterDatas_FabricWidthId",
                table: "PurchaseEntryDetails",
                column: "FabricWidthId",
                principalTable: "MasterDatas",
                principalColumn: "MasterDataId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseEntryDetails_MasterDatas_ItemId",
                table: "PurchaseEntryDetails",
                column: "ItemId",
                principalTable: "MasterDatas",
                principalColumn: "MasterDataId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
