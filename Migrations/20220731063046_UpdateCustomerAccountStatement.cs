using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class UpdateCustomerAccountStatement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountStatements_CustomerId",
                table: "CustomerAccountStatements",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountStatements_Customers_CustomerId",
                table: "CustomerAccountStatements",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountStatements_Customers_CustomerId",
                table: "CustomerAccountStatements");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccountStatements_CustomerId",
                table: "CustomerAccountStatements");
        }
    }
}
