using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class update_PETable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseEntryDetails_MasterDatas_BrandId",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseEntryDetails_BrandId",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropColumn(
                name: "TotalPaid",
                table: "PurchaseEntryDetails");

            migrationBuilder.DropColumn(
                name: "ContactNo",
                table: "PurchaseEntries");

            migrationBuilder.DropColumn(
                name: "TRN",
                table: "PurchaseEntries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "PurchaseEntryDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "PurchaseEntryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPaid",
                table: "PurchaseEntryDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ContactNo",
                table: "PurchaseEntries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TRN",
                table: "PurchaseEntries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntryDetails_BrandId",
                table: "PurchaseEntryDetails",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseEntryDetails_MasterDatas_BrandId",
                table: "PurchaseEntryDetails",
                column: "BrandId",
                principalTable: "MasterDatas",
                principalColumn: "MasterDataId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
