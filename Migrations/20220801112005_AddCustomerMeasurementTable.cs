using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MKExpress.Web.API.Migrations
{
    public partial class AddCustomerMeasurementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DesignSampleId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Chest = table.Column<int>(type: "int", nullable: false),
                    SleeveLoose = table.Column<int>(type: "int", nullable: false),
                    Deep = table.Column<int>(type: "int", nullable: false),
                    BackDown = table.Column<int>(type: "int", nullable: false),
                    Bottom = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Hipps = table.Column<int>(type: "int", nullable: false),
                    Sleeve = table.Column<int>(type: "int", nullable: false),
                    Shoulder = table.Column<int>(type: "int", nullable: false),
                    Neck = table.Column<int>(type: "int", nullable: false),
                    Waist = table.Column<int>(type: "int", nullable: false),
                    Extra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerMeasurements_Customers_DesignSampleId",
                        column: x => x.DesignSampleId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMeasurements_DesignSampleId",
                table: "CustomerMeasurements",
                column: "DesignSampleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerMeasurements");
        }
    }
}
