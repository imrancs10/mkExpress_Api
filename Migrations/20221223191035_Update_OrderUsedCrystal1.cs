using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_OrderUsedCrystal1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderUsedCrystals_Products_ProductStockId",
                table: "OrderUsedCrystals");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUsedCrystals_ProductStocks_ProductStockId",
                table: "OrderUsedCrystals",
                column: "ProductStockId",
                principalTable: "ProductStocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderUsedCrystals_ProductStocks_ProductStockId",
                table: "OrderUsedCrystals");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUsedCrystals_Products_ProductStockId",
                table: "OrderUsedCrystals",
                column: "ProductStockId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
