using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_OrderWorkDescription_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LikePrint",
                table: "OrderWorkDescriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewPrint",
                table: "OrderWorkDescriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SamePrint",
                table: "OrderWorkDescriptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikePrint",
                table: "OrderWorkDescriptions");

            migrationBuilder.DropColumn(
                name: "NewPrint",
                table: "OrderWorkDescriptions");

            migrationBuilder.DropColumn(
                name: "SamePrint",
                table: "OrderWorkDescriptions");
        }
    }
}
