using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_CustomerAccStatement_AddIsFirstAdvance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "DeliveredQty",
            //    table: "CustomerAccountStatements",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstAdvance",
                table: "CustomerAccountStatements",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveredQty",
                table: "CustomerAccountStatements");

            migrationBuilder.DropColumn(
                name: "IsFirstAdvance",
                table: "CustomerAccountStatements");
        }
    }
}
