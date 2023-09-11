using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class update_PStockTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_MasterDatas_BrandId",
                table: "ProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_ProductStocks_BrandId",
                table: "ProductStocks");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "ProductStocks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "ProductStocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_BrandId",
                table: "ProductStocks",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_MasterDatas_BrandId",
                table: "ProductStocks",
                column: "BrandId",
                principalTable: "MasterDatas",
                principalColumn: "MasterDataId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
