using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_ExpenseTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseShopCompanies_CompanyId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Expenses",
                type: "int",
                nullable: true,
                defaultValue:null,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseShopCompanies_CompanyId",
                table: "Expenses",
                column: "CompanyId",
                principalTable: "ExpenseShopCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseShopCompanies_CompanyId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseShopCompanies_CompanyId",
                table: "Expenses",
                column: "CompanyId",
                principalTable: "ExpenseShopCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
