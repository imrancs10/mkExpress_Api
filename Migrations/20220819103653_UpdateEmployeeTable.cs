using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class UpdateEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_MasterExperts_ExpertyId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ExpertyId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ExpertyId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "AadharNo",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact2",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ExpertId",
                table: "Employees",
                column: "ExpertId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_MasterExperts_ExpertId",
                table: "Employees",
                column: "ExpertId",
                principalTable: "MasterExperts",
                principalColumn: "ExpertId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_MasterExperts_ExpertId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ExpertId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AadharNo",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Contact2",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "ExpertyId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ExpertyId",
                table: "Employees",
                column: "ExpertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_MasterExperts_ExpertyId",
                table: "Employees",
                column: "ExpertyId",
                principalTable: "MasterExperts",
                principalColumn: "ExpertId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
