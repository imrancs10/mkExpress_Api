using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class UpdateCustomerMeasurementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerMeasurements_Customers_DesignSampleId",
                table: "CustomerMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_CustomerMeasurements_DesignSampleId",
                table: "CustomerMeasurements");

            migrationBuilder.DropColumn(
                name: "DesignSampleId",
                table: "CustomerMeasurements");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMeasurements_CustomerId",
                table: "CustomerMeasurements",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerMeasurements_Customers_CustomerId",
                table: "CustomerMeasurements",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerMeasurements_Customers_CustomerId",
                table: "CustomerMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_CustomerMeasurements_CustomerId",
                table: "CustomerMeasurements");

            migrationBuilder.AddColumn<int>(
                name: "DesignSampleId",
                table: "CustomerMeasurements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMeasurements_DesignSampleId",
                table: "CustomerMeasurements",
                column: "DesignSampleId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerMeasurements_Customers_DesignSampleId",
                table: "CustomerMeasurements",
                column: "DesignSampleId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
