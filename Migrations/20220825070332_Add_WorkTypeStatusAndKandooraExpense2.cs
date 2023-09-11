using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Add_WorkTypeStatusAndKandooraExpense2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EachKandooraExpenses_KandooraHeadId",
                table: "EachKandooraExpenses",
                column: "KandooraHeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_EachKandooraExpenses_EachKandooraExpenseHeads_KandooraHeadId",
                table: "EachKandooraExpenses",
                column: "KandooraHeadId",
                principalTable: "EachKandooraExpenseHeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EachKandooraExpenses_EachKandooraExpenseHeads_KandooraHeadId",
                table: "EachKandooraExpenses");

            migrationBuilder.DropIndex(
                name: "IX_EachKandooraExpenses_KandooraHeadId",
                table: "EachKandooraExpenses");
        }
    }
}
