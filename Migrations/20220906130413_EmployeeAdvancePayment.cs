using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MKExpress.Web.API.Migrations
{
    public partial class EmployeeAdvancePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accomdation",
                table: "MonthlyAttendences");

            migrationBuilder.DropColumn(
                name: "BasicSalary",
                table: "MonthlyAttendences");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "MonthlyAttendences");

            migrationBuilder.DropColumn(
                name: "Month_Salary",
                table: "MonthlyAttendences");

            migrationBuilder.DropColumn(
                name: "TotalNet",
                table: "MonthlyAttendences");

            migrationBuilder.DropColumn(
                name: "TotalSalary",
                table: "MonthlyAttendences");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeAdvancePayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EMI = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAdvancePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAdvancePayments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEMIPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvancePaymentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeductionMonth = table.Column<int>(type: "int", nullable: false),
                    DeductionYear = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEMIPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeEMIPayments_EmployeeAdvancePayments_AdvancePaymentId",
                        column: x => x.AdvancePaymentId,
                        principalTable: "EmployeeAdvancePayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAdvancePayments_EmployeeId",
                table: "EmployeeAdvancePayments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEMIPayments_AdvancePaymentId",
                table: "EmployeeEMIPayments",
                column: "AdvancePaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEMIPayments");

            migrationBuilder.DropTable(
                name: "EmployeeAdvancePayments");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.AddColumn<decimal>(
                name: "Accomdation",
                table: "MonthlyAttendences",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BasicSalary",
                table: "MonthlyAttendences",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "MonthlyAttendences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Month_Salary",
                table: "MonthlyAttendences",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalNet",
                table: "MonthlyAttendences",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSalary",
                table: "MonthlyAttendences",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
