using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class updateOrderDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_DesignSamples_DesignSampleId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "DesignSampleId",
                table: "OrderDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_DesignSamples_DesignSampleId",
                table: "OrderDetails",
                column: "DesignSampleId",
                principalTable: "DesignSamples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_DesignSamples_DesignSampleId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "DesignSampleId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_DesignSamples_DesignSampleId",
                table: "OrderDetails",
                column: "DesignSampleId",
                principalTable: "DesignSamples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
