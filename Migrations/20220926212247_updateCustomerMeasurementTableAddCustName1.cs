using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class updateCustomerMeasurementTableAddCustName1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "OrderDetails",
                newName: "MeasurementCustomerName");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "CustomerMeasurements",
                newName: "MeasurementCustomerName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeasurementCustomerName",
                table: "OrderDetails",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "MeasurementCustomerName",
                table: "CustomerMeasurements",
                newName: "CustomerName");
        }
    }
}
