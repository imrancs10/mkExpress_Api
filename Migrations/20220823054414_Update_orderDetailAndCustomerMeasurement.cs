using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_orderDetailAndCustomerMeasurement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cuff",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cuff",
                table: "CustomerMeasurements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntries_CreatedBy",
                table: "PurchaseEntries",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseEntries_Employees_CreatedBy",
                table: "PurchaseEntries",
                column: "CreatedBy",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseEntries_Employees_CreatedBy",
                table: "PurchaseEntries");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseEntries_CreatedBy",
                table: "PurchaseEntries");

            migrationBuilder.DropColumn(
                name: "Cuff",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Cuff",
                table: "CustomerMeasurements");
        }
    }
}
