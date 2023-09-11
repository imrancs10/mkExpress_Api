using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_AccountStatement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "CustomerAccountStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
            migrationBuilder.AddColumn<int>(
                name: "DeliveredQty",
                table: "CustomerAccountStatements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "CustomerAccountStatements");
            migrationBuilder.DropColumn(
               name: "DeliveredQty",
               table: "CustomerAccountStatements");
        }
    }
}
