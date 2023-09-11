using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_EmployeeAndRemoveJobExpert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_MasterExperts_ExpertId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "MasterExperts");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ExpertId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ExpertId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "DesignSamples");

            migrationBuilder.RenameColumn(
                name: "AadharNo",
                table: "Employees",
                newName: "LabourId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFixedEmployee",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LabourIdExpire",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFixedEmployee",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LabourIdExpire",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "LabourId",
                table: "Employees",
                newName: "AadharNo");

            migrationBuilder.AddColumn<int>(
                name: "ExpertId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "DesignSamples",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MasterExperts",
                columns: table => new
                {
                    ExpertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterExperts", x => x.ExpertId);
                });

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
    }
}
