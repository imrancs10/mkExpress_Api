using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_MasterDatas_SizeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_MasterDatas_WidthId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "WidthId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MasterDatas_SizeId",
                table: "Products",
                column: "SizeId",
                principalTable: "MasterDatas",
                principalColumn: "MasterDataId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MasterDatas_WidthId",
                table: "Products",
                column: "WidthId",
                principalTable: "MasterDatas",
                principalColumn: "MasterDataId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_MasterDatas_SizeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_MasterDatas_WidthId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "WidthId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MasterDatas_SizeId",
                table: "Products",
                column: "SizeId",
                principalTable: "MasterDatas",
                principalColumn: "MasterDataId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MasterDatas_WidthId",
                table: "Products",
                column: "WidthId",
                principalTable: "MasterDatas",
                principalColumn: "MasterDataId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
