using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class UpdateOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CrystalStatus",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CrystalUsed",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CuttingStatus",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "HandembStatus",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "PackStatus",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "StitchStatus",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "OrderDetails",
                newName: "Crystal");

            migrationBuilder.AddColumn<decimal>(
                name: "VAT",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Crystal",
                table: "OrderDetails",
                newName: "Type");

            migrationBuilder.AddColumn<decimal>(
                name: "CrystalStatus",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CrystalUsed",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CuttingStatus",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HandembStatus",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PackStatus",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StitchStatus",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
