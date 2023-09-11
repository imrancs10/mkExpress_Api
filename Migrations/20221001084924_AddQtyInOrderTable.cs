using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class AddQtyInOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cuff",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Cuff",
                table: "CustomerMeasurements");

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "Orders",
                type: "int",
                nullable: true,
                defaultValue: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Cuff",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cuff",
                table: "CustomerMeasurements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
