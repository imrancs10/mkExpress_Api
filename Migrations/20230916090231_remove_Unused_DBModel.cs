using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.API.Migrations
{
    public partial class remove_Unused_DBModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountStatements_OrderDetails_OrderDetailId",
                table: "CustomerAccountStatements");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountStatements_Orders_OrderId",
                table: "CustomerAccountStatements");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionResources_ResourceTypes_ResourceTypeId",
                table: "PermissionResources");

            migrationBuilder.DropTable(
                name: "CancelledOrders");

            migrationBuilder.DropTable(
                name: "CrystalPurchaseDetails");

            migrationBuilder.DropTable(
                name: "CrystalPurchaseInstallments");

            migrationBuilder.DropTable(
                name: "CrystalStocks");

            migrationBuilder.DropTable(
                name: "CrystalStockUpdateHistories");

            migrationBuilder.DropTable(
                name: "CrystalTrackingOutDetails");

            migrationBuilder.DropTable(
                name: "CustomerMeasurements");

            migrationBuilder.DropTable(
                name: "CuttingMasters");

            migrationBuilder.DropTable(
                name: "EachKandooraExpenses");

            migrationBuilder.DropTable(
                name: "EmployeeEMIPayments");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "MasterHolidays");

            migrationBuilder.DropTable(
                name: "MonthlyAttendences");

            migrationBuilder.DropTable(
                name: "OrderCrystalDetails");

            migrationBuilder.DropTable(
                name: "OrderUsedCrystals");

            migrationBuilder.DropTable(
                name: "OrderWorkDescriptions");

            migrationBuilder.DropTable(
                name: "PurchaseEntryDetails");

            migrationBuilder.DropTable(
                name: "RentTransations");

            migrationBuilder.DropTable(
                name: "WorkTypeStatuses");

            migrationBuilder.DropTable(
                name: "CrystalPurchases");

            migrationBuilder.DropTable(
                name: "CrystalTrackingOuts");

            migrationBuilder.DropTable(
                name: "MasterCrystals");

            migrationBuilder.DropTable(
                name: "EachKandooraExpenseHeads");

            migrationBuilder.DropTable(
                name: "EmployeeAdvancePayments");

            migrationBuilder.DropTable(
                name: "ExpenseNames");

            migrationBuilder.DropTable(
                name: "ExpenseShopCompanies");

            migrationBuilder.DropTable(
                name: "MasterHolidayNames");

            migrationBuilder.DropTable(
                name: "ProductStocks");

            migrationBuilder.DropTable(
                name: "MasterWorkDescriptions");

            migrationBuilder.DropTable(
                name: "PurchaseEntries");

            migrationBuilder.DropTable(
                name: "RentDetails");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");

            migrationBuilder.DropTable(
                name: "MasterHolidayTypes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "RentLocations");

            migrationBuilder.DropTable(
                name: "DesignSamples");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "MasterDesignCategories");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccountStatements_OrderDetailId",
                table: "CustomerAccountStatements");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccountStatements_OrderId",
                table: "CustomerAccountStatements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceTypes",
                table: "ResourceTypes");

            migrationBuilder.RenameTable(
                name: "ResourceTypes",
                newName: "ResourceType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceType",
                table: "ResourceType",
                column: "ResourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionResources_ResourceType_ResourceTypeId",
                table: "PermissionResources",
                column: "ResourceTypeId",
                principalTable: "ResourceType",
                principalColumn: "ResourceTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionResources_ResourceType_ResourceTypeId",
                table: "PermissionResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceType",
                table: "ResourceType");

            migrationBuilder.RenameTable(
                name: "ResourceType",
                newName: "ResourceTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceTypes",
                table: "ResourceTypes",
                column: "ResourceTypeId");

            migrationBuilder.CreateTable(
                name: "CustomerMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BackDown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Deep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hipps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementCustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Neck = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shoulder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sleeve = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SleeveLoose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Waist = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerMeasurements_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuttingMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CutttingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    F_Fabric = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loosing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Neck = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderPic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Shoulder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sleeves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuttingMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EachKandooraExpenseHeads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    HeadName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EachKandooraExpenseHeads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAdvancePayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    EMI = table.Column<int>(type: "int", nullable: false),
                    EMIStartMonth = table.Column<int>(type: "int", nullable: false),
                    EMIStartYear = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAdvancePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAdvancePayments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseShopCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseShopCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_ExpenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterCrystals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertQty = table.Column<int>(type: "int", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CrystalId = table.Column<int>(type: "int", nullable: false),
                    IsArtical = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QtyPerPacket = table.Column<int>(type: "int", nullable: false),
                    ShapeId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCrystals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterCrystals_MasterDatas_BrandId",
                        column: x => x.BrandId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterCrystals_MasterDatas_ShapeId",
                        column: x => x.ShapeId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterCrystals_MasterDatas_SizeId",
                        column: x => x.SizeId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterDesignCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_MasterDesignCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "MasterHolidayTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_MasterHolidayTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterWorkDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_MasterWorkDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyAttendences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Advance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Day1 = table.Column<int>(type: "int", nullable: false),
                    Day10 = table.Column<int>(type: "int", nullable: false),
                    Day11 = table.Column<int>(type: "int", nullable: false),
                    Day12 = table.Column<int>(type: "int", nullable: false),
                    Day13 = table.Column<int>(type: "int", nullable: false),
                    Day14 = table.Column<int>(type: "int", nullable: false),
                    Day15 = table.Column<int>(type: "int", nullable: false),
                    Day16 = table.Column<int>(type: "int", nullable: false),
                    Day17 = table.Column<int>(type: "int", nullable: false),
                    Day18 = table.Column<int>(type: "int", nullable: false),
                    Day19 = table.Column<int>(type: "int", nullable: false),
                    Day2 = table.Column<int>(type: "int", nullable: false),
                    Day20 = table.Column<int>(type: "int", nullable: false),
                    Day21 = table.Column<int>(type: "int", nullable: false),
                    Day22 = table.Column<int>(type: "int", nullable: false),
                    Day23 = table.Column<int>(type: "int", nullable: false),
                    Day24 = table.Column<int>(type: "int", nullable: false),
                    Day25 = table.Column<int>(type: "int", nullable: false),
                    Day26 = table.Column<int>(type: "int", nullable: false),
                    Day27 = table.Column<int>(type: "int", nullable: false),
                    Day28 = table.Column<int>(type: "int", nullable: false),
                    Day29 = table.Column<int>(type: "int", nullable: false),
                    Day3 = table.Column<int>(type: "int", nullable: false),
                    Day30 = table.Column<int>(type: "int", nullable: false),
                    Day31 = table.Column<int>(type: "int", nullable: false),
                    Day4 = table.Column<int>(type: "int", nullable: false),
                    Day5 = table.Column<int>(type: "int", nullable: false),
                    Day6 = table.Column<int>(type: "int", nullable: false),
                    Day7 = table.Column<int>(type: "int", nullable: false),
                    Day8 = table.Column<int>(type: "int", nullable: false),
                    Day9 = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    OverTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyAttendences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyAttendences_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvanceVATAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BalanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CancelledAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CancelledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CustomerRefName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxInvoiceNo = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VATAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TRN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "EachKandooraExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    KandooraHeadId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EachKandooraExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EachKandooraExpenses_EachKandooraExpenseHeads_KandooraHeadId",
                        column: x => x.KandooraHeadId,
                        principalTable: "EachKandooraExpenseHeads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEMIPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvancePaymentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    DeductionMonth = table.Column<int>(type: "int", nullable: false),
                    DeductionYear = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEMIPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeEMIPayments_EmployeeAdvancePayments_AdvancePaymentId",
                        column: x => x.AdvancePaymentId,
                        principalTable: "EmployeeAdvancePayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ExpenseTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseNames_ExpenseTypes_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "ExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrystalStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BalanceStock = table.Column<int>(type: "int", nullable: false),
                    BalanceStockPieces = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CrystalId = table.Column<int>(type: "int", nullable: false),
                    InStock = table.Column<int>(type: "int", nullable: false),
                    InStockPieces = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OutStock = table.Column<int>(type: "int", nullable: false),
                    OutStockPieces = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalStocks_MasterCrystals_CrystalId",
                        column: x => x.CrystalId,
                        principalTable: "MasterCrystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrystalStockUpdateHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CrystalId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NewPackets = table.Column<int>(type: "int", nullable: false),
                    NewPieces = table.Column<int>(type: "int", nullable: false),
                    OldPackets = table.Column<int>(type: "int", nullable: false),
                    OldPieces = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalStockUpdateHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalStockUpdateHistories_MasterCrystals_CrystalId",
                        column: x => x.CrystalId,
                        principalTable: "MasterCrystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    DesignerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Shape = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignSamples_MasterDesignCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MasterDesignCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterHolidayNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    HolidayTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
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
                name: "CancelledOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OrderBDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrderVat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelledOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelledOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    WidthId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_MasterDatas_SizeId",
                        column: x => x.SizeId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_MasterDatas_WidthId",
                        column: x => x.WidthId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    FirstInstallmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Installments = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RentLocationId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentDetails_RentLocations_RentLocationId",
                        column: x => x.RentLocationId,
                        principalTable: "RentLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrystalPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChequeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChequeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    InstallmentStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Installments = table.Column<int>(type: "int", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsWithOutVat = table.Column<bool>(type: "bit", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseNo = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    SubTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalPurchases_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseEntries",
                columns: table => new
                {
                    PurchaseEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseNo = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseEntries", x => x.PurchaseEntryId);
                    table.ForeignKey(
                        name: "FK_PurchaseEntries_Employees_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseEntries_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpJobTitle = table.Column<int>(type: "int", nullable: true),
                    EmpJobTitleId = table.Column<int>(type: "int", nullable: true),
                    EmplopeeId = table.Column<int>(type: "int", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseNameId = table.Column<int>(type: "int", nullable: false),
                    ExpenseNo = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<int>(type: "int", nullable: false),
                    ReferenceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Employees_EmplopeeId",
                        column: x => x.EmplopeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseNames_ExpenseNameId",
                        column: x => x.ExpenseNameId,
                        principalTable: "ExpenseNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseShopCompanies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "ExpenseShopCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_MasterJobTitles_EmpJobTitle",
                        column: x => x.EmpJobTitle,
                        principalTable: "MasterJobTitles",
                        principalColumn: "JobTitleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BackDown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bottom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Chest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Crystal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrystalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignSampleId = table.Column<int>(type: "int", nullable: true),
                    Extra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hipps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementCustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Neck = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Shoulder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sleeve = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SleeveLoose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Waist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_DesignSamples_DesignSampleId",
                        column: x => x.DesignSampleId,
                        principalTable: "DesignSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Employees_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Employees_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterHolidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    HolidayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HolidayNameId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RecurringEveryYear = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ProductStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvailablePiece = table.Column<int>(type: "int", nullable: false),
                    AvailableQty = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UsedPiece = table.Column<int>(type: "int", nullable: false),
                    UsedQty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductStocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentTransations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChequeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    InstallmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InstallmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstallmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidBy = table.Column<int>(type: "int", nullable: true),
                    PaidOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentDetailId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentTransations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentTransations_Employees_PaidBy",
                        column: x => x.PaidBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentTransations_RentDetails_RentDetailId",
                        column: x => x.RentDetailId,
                        principalTable: "RentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrystalPurchaseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CrystalId = table.Column<int>(type: "int", nullable: false),
                    CrystalPurchaseId = table.Column<int>(type: "int", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PiecePerPacket = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    SubTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPiece = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalPurchaseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalPurchaseDetails_CrystalPurchases_CrystalPurchaseId",
                        column: x => x.CrystalPurchaseId,
                        principalTable: "CrystalPurchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrystalPurchaseDetails_MasterCrystals_CrystalId",
                        column: x => x.CrystalId,
                        principalTable: "MasterCrystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrystalPurchaseInstallments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CrystalPurchaseId = table.Column<int>(type: "int", nullable: false),
                    InstallmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstallmentNo = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalPurchaseInstallments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalPurchaseInstallments_CrystalPurchases_CrystalPurchaseId",
                        column: x => x.CrystalPurchaseId,
                        principalTable: "CrystalPurchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseEntryDetails",
                columns: table => new
                {
                    PurchaseEntryDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseEntryId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseEntryDetails", x => x.PurchaseEntryDetailId);
                    table.ForeignKey(
                        name: "FK_PurchaseEntryDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseEntryDetails_PurchaseEntries_PurchaseEntryId",
                        column: x => x.PurchaseEntryId,
                        principalTable: "PurchaseEntries",
                        principalColumn: "PurchaseEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrystalTrackingOuts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalTrackingOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalTrackingOuts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrystalTrackingOuts_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderWorkDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LikeModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    SamePrint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    WorkDescriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderWorkDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderWorkDescriptions_MasterWorkDescriptions_WorkDescriptionId",
                        column: x => x.WorkDescriptionId,
                        principalTable: "MasterWorkDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderWorkDescriptions_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypeStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddtionalNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletedBy = table.Column<int>(type: "int", nullable: true),
                    CompletedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Extra = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsSaved = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    VoucherNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypeStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkTypeStatuses_Employees_CompletedBy",
                        column: x => x.CompletedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkTypeStatuses_Employees_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkTypeStatuses_Employees_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkTypeStatuses_MasterDatas_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "MasterDatas",
                        principalColumn: "MasterDataId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkTypeStatuses_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderCrystalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductStockId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UsedPiece = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCrystalDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderCrystalDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderCrystalDetails_ProductStocks_ProductStockId",
                        column: x => x.ProductStockId,
                        principalTable: "ProductStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderUsedCrystals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    ProductStockId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UsedQty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderUsedCrystals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderUsedCrystals_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderUsedCrystals_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderUsedCrystals_ProductStocks_ProductStockId",
                        column: x => x.ProductStockId,
                        principalTable: "ProductStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrystalTrackingOutDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticalLabourCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CrystalId = table.Column<int>(type: "int", nullable: false),
                    CrystalLabourCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAlterWork = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LoosePieces = table.Column<int>(type: "int", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReleasePacketQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReleasePieceQty = table.Column<int>(type: "int", nullable: false),
                    TrackingOutId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalTrackingOutDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalTrackingOutDetails_CrystalTrackingOuts_TrackingOutId",
                        column: x => x.TrackingOutId,
                        principalTable: "CrystalTrackingOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrystalTrackingOutDetails_MasterCrystals_CrystalId",
                        column: x => x.CrystalId,
                        principalTable: "MasterCrystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrystalTrackingOutDetails_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountStatements_OrderDetailId",
                table: "CustomerAccountStatements",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountStatements_OrderId",
                table: "CustomerAccountStatements",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelledOrders_OrderId",
                table: "CancelledOrders",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrystalPurchaseDetails_CrystalId",
                table: "CrystalPurchaseDetails",
                column: "CrystalId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalPurchaseDetails_CrystalPurchaseId",
                table: "CrystalPurchaseDetails",
                column: "CrystalPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalPurchaseInstallments_CrystalPurchaseId",
                table: "CrystalPurchaseInstallments",
                column: "CrystalPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalPurchases_SupplierId",
                table: "CrystalPurchases",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalStocks_CrystalId",
                table: "CrystalStocks",
                column: "CrystalId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalStockUpdateHistories_CrystalId",
                table: "CrystalStockUpdateHistories",
                column: "CrystalId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOutDetails_CrystalId",
                table: "CrystalTrackingOutDetails",
                column: "CrystalId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOutDetails_OrderDetailId",
                table: "CrystalTrackingOutDetails",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOutDetails_TrackingOutId",
                table: "CrystalTrackingOutDetails",
                column: "TrackingOutId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOuts_EmployeeId",
                table: "CrystalTrackingOuts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalTrackingOuts_OrderDetailId",
                table: "CrystalTrackingOuts",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMeasurements_CustomerId",
                table: "CustomerMeasurements",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignSamples_CategoryId",
                table: "DesignSamples",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EachKandooraExpenses_KandooraHeadId",
                table: "EachKandooraExpenses",
                column: "KandooraHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAdvancePayments_EmployeeId",
                table: "EmployeeAdvancePayments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEMIPayments_AdvancePaymentId",
                table: "EmployeeEMIPayments",
                column: "AdvancePaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseNames_ExpenseTypeId",
                table: "ExpenseNames",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CompanyId",
                table: "Expenses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_EmpJobTitle",
                table: "Expenses",
                column: "EmpJobTitle");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_EmplopeeId",
                table: "Expenses",
                column: "EmplopeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseNameId",
                table: "Expenses",
                column: "ExpenseNameId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCrystals_BrandId",
                table: "MasterCrystals",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCrystals_ShapeId",
                table: "MasterCrystals",
                column: "ShapeId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCrystals_SizeId",
                table: "MasterCrystals",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHolidayNames_HolidayTypeId",
                table: "MasterHolidayNames",
                column: "HolidayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHolidays_HolidayNameId",
                table: "MasterHolidays",
                column: "HolidayNameId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyAttendences_EmployeeId",
                table: "MonthlyAttendences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCrystalDetails_ProductId",
                table: "OrderCrystalDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCrystalDetails_ProductStockId",
                table: "OrderCrystalDetails",
                column: "ProductStockId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_CreatedBy",
                table: "OrderDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_DesignSampleId",
                table: "OrderDetails",
                column: "DesignSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_UpdatedBy",
                table: "OrderDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedBy",
                table: "Orders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UpdatedBy",
                table: "Orders",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsedCrystals_EmployeeId",
                table: "OrderUsedCrystals",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsedCrystals_OrderDetailId",
                table: "OrderUsedCrystals",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsedCrystals_ProductStockId",
                table: "OrderUsedCrystals",
                column: "ProductStockId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderWorkDescriptions_OrderDetailId",
                table: "OrderWorkDescriptions",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderWorkDescriptions_WorkDescriptionId",
                table: "OrderWorkDescriptions",
                column: "WorkDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeId",
                table: "Products",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_WidthId",
                table: "Products",
                column: "WidthId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_ProductId",
                table: "ProductStocks",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntries_CreatedBy",
                table: "PurchaseEntries",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntries_SupplierId",
                table: "PurchaseEntries",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntryDetails_ProductId",
                table: "PurchaseEntryDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntryDetails_PurchaseEntryId",
                table: "PurchaseEntryDetails",
                column: "PurchaseEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_RentDetails_RentLocationId",
                table: "RentDetails",
                column: "RentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RentTransations_PaidBy",
                table: "RentTransations",
                column: "PaidBy");

            migrationBuilder.CreateIndex(
                name: "IX_RentTransations_RentDetailId",
                table: "RentTransations",
                column: "RentDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypeStatuses_CompletedBy",
                table: "WorkTypeStatuses",
                column: "CompletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypeStatuses_CreatedBy",
                table: "WorkTypeStatuses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypeStatuses_OrderDetailId",
                table: "WorkTypeStatuses",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypeStatuses_UpdatedBy",
                table: "WorkTypeStatuses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypeStatuses_WorkTypeId",
                table: "WorkTypeStatuses",
                column: "WorkTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountStatements_OrderDetails_OrderDetailId",
                table: "CustomerAccountStatements",
                column: "OrderDetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountStatements_Orders_OrderId",
                table: "CustomerAccountStatements",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionResources_ResourceTypes_ResourceTypeId",
                table: "PermissionResources",
                column: "ResourceTypeId",
                principalTable: "ResourceTypes",
                principalColumn: "ResourceTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
