using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_RentTables1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentTransations_Employees_PaidBy",
                table: "RentTransations");

            migrationBuilder.AlterColumn<int>(
                name: "PaidBy",
                table: "RentTransations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RentTransations_Employees_PaidBy",
                table: "RentTransations",
                column: "PaidBy",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentTransations_Employees_PaidBy",
                table: "RentTransations");

            migrationBuilder.AlterColumn<int>(
                name: "PaidBy",
                table: "RentTransations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RentTransations_Employees_PaidBy",
                table: "RentTransations",
                column: "PaidBy",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
