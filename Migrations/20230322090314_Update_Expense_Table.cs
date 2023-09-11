using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_Expense_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CrystalTrackingOuts_OrderDetailId",
                table: "CrystalTrackingOuts");

            migrationBuilder.AddColumn<int>(
                name: "ReferenceId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceName",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOuts_OrderDetailId",
                table: "CrystalTrackingOuts",
                column: "OrderDetailId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CrystalTrackingOuts_OrderDetailId",
                table: "CrystalTrackingOuts");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "ReferenceName",
                table: "Expenses");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOuts_OrderDetailId",
                table: "CrystalTrackingOuts",
                column: "OrderDetailId");
        }
    }
}
