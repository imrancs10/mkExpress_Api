using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_OrderCrystal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCrystalDetails_Products_ProductId",
                table: "OrderCrystalDetails");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "ProductStocks",
                type: "int",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderCrystalDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductStockId",
                table: "OrderCrystalDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_BrandId",
                table: "ProductStocks",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCrystalDetails_ProductStockId",
                table: "OrderCrystalDetails",
                column: "ProductStockId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCrystalDetails_Products_ProductId",
                table: "OrderCrystalDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCrystalDetails_ProductStocks_ProductStockId",
                table: "OrderCrystalDetails",
                column: "ProductStockId",
                principalTable: "ProductStocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_MasterDatas_BrandId",
                table: "ProductStocks",
                column: "BrandId",
                principalTable: "MasterDatas",
                principalColumn: "MasterDataId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCrystalDetails_Products_ProductId",
                table: "OrderCrystalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderCrystalDetails_ProductStocks_ProductStockId",
                table: "OrderCrystalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_MasterDatas_BrandId",
                table: "ProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_ProductStocks_BrandId",
                table: "ProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_OrderCrystalDetails_ProductStockId",
                table: "OrderCrystalDetails");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "ProductStocks");

            migrationBuilder.DropColumn(
                name: "ProductStockId",
                table: "OrderCrystalDetails");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderCrystalDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCrystalDetails_Products_ProductId",
                table: "OrderCrystalDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
