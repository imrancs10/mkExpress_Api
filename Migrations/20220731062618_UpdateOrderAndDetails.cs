using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class UpdateOrderAndDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailId",
                table: "CustomerAccountStatements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountStatements_OrderDetailId",
                table: "CustomerAccountStatements",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountStatements_OrderId",
                table: "CustomerAccountStatements",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountStatements_OrderDetails_OrderDetailId",
                table: "CustomerAccountStatements",
                column: "OrderDetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountStatements_Orders_OrderId",
                table: "CustomerAccountStatements",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountStatements_OrderDetails_OrderDetailId",
                table: "CustomerAccountStatements");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountStatements_Orders_OrderId",
                table: "CustomerAccountStatements");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccountStatements_OrderDetailId",
                table: "CustomerAccountStatements");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccountStatements_OrderId",
                table: "CustomerAccountStatements");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailId",
                table: "CustomerAccountStatements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
