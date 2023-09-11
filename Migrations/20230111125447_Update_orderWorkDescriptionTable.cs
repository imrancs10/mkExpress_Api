using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_orderWorkDescriptionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewPrint",
                table: "OrderWorkDescriptions",
                newName: "NewModel");

            migrationBuilder.RenameColumn(
                name: "LikePrint",
                table: "OrderWorkDescriptions",
                newName: "LikeModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewModel",
                table: "OrderWorkDescriptions",
                newName: "NewPrint");

            migrationBuilder.RenameColumn(
                name: "LikeModel",
                table: "OrderWorkDescriptions",
                newName: "LikePrint");
        }
    }
}
