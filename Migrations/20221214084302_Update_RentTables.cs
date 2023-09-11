using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_RentTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentTransations_RentDetails_PaidBy",
                table: "RentTransations");

            migrationBuilder.AddForeignKey(
                name: "FK_RentTransations_Employees_PaidBy",
                table: "RentTransations",
                column: "PaidBy",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentTransations_Employees_PaidBy",
                table: "RentTransations");

            migrationBuilder.AddForeignKey(
                name: "FK_RentTransations_RentDetails_PaidBy",
                table: "RentTransations",
                column: "PaidBy",
                principalTable: "RentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
