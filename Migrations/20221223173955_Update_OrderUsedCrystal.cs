using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_OrderUsedCrystal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderUsedCrystals_Products_ProductId",
                table: "OrderUsedCrystals");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderUsedCrystals",
                newName: "ProductStockId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderUsedCrystals_ProductId",
                table: "OrderUsedCrystals",
                newName: "IX_OrderUsedCrystals_ProductStockId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "OrderUsedCrystals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsedCrystals_EmployeeId",
                table: "OrderUsedCrystals",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUsedCrystals_Employees_EmployeeId",
                table: "OrderUsedCrystals",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUsedCrystals_Products_ProductStockId",
                table: "OrderUsedCrystals",
                column: "ProductStockId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderUsedCrystals_Employees_EmployeeId",
                table: "OrderUsedCrystals");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderUsedCrystals_Products_ProductStockId",
                table: "OrderUsedCrystals");

            migrationBuilder.DropIndex(
                name: "IX_OrderUsedCrystals_EmployeeId",
                table: "OrderUsedCrystals");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "OrderUsedCrystals");

            migrationBuilder.RenameColumn(
                name: "ProductStockId",
                table: "OrderUsedCrystals",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderUsedCrystals_ProductStockId",
                table: "OrderUsedCrystals",
                newName: "IX_OrderUsedCrystals_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUsedCrystals_Products_ProductId",
                table: "OrderUsedCrystals",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
