using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MKExpress.Web.API.Migrations
{
    public partial class Create_HolidayTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advances");

            migrationBuilder.DropTable(
                name: "AlterDetails");

            migrationBuilder.DropTable(
                name: "AlterMasters");

            migrationBuilder.DropTable(
                name: "AlterPackingLists");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.CreateTable(
                name: "MasterHolidayTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterHolidayTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterHolidayNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolidayTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterHolidayNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterHolidayNames_MasterHolidayTypes_HolidayTypeId",
                        column: x => x.HolidayTypeId,
                        principalTable: "MasterHolidayTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterHolidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    HolidayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecurringEveryYear = table.Column<bool>(type: "bit", nullable: false),
                    HolidayNameId = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterHolidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterHolidays_MasterHolidayNames_HolidayNameId",
                        column: x => x.HolidayNameId,
                        principalTable: "MasterHolidayNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterHolidayNames_HolidayTypeId",
                table: "MasterHolidayNames",
                column: "HolidayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHolidays_HolidayNameId",
                table: "MasterHolidays",
                column: "HolidayNameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterHolidays");

            migrationBuilder.DropTable(
                name: "MasterHolidayNames");

            migrationBuilder.DropTable(
                name: "MasterHolidayTypes");

            migrationBuilder.CreateTable(
                name: "Advances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlterDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Al_Ord_No = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Alt_Id = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BackDown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CrystalUsed = table.Column<float>(type: "real", nullable: true),
                    Crystal_Status = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Cus_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cut_Stauts = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Cut_Work = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Deep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Del_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Free = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Handemb_Status = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Hipps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Kn_id = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MEMB_Status = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Neck = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ord_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pack_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Patch_Work = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Saleev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleevLoosing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shoulder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statch_status = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Waist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Work_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qsize = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlterDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlterMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Al_Ord_No = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AltPrefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alt_Id = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BackDown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bal_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Cus_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cus_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Del_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Del_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Free = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hipps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Neck = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ord_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Paid_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Saleev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleevLoosing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Saleman = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shoulder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Waist = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlterMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlterPackingLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alter_No = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Balance_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Kn_Id = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlterPackingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bank_Id = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BRANCH_LOCATION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BRANCH_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BRNCH_PREFIX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch_id = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });
        }
    }
}
