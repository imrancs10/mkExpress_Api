using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Add_CrystalInstallments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TRN",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CrystalPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseNo = table.Column<int>(type: "int", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    SubTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChequeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChequeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstallmentStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsWithOutVat = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                name: "CrystalStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrystalId = table.Column<int>(type: "int", nullable: false),
                    InStock = table.Column<int>(type: "int", nullable: false),
                    InStockPieces = table.Column<int>(type: "int", nullable: false),
                    OutStock = table.Column<int>(type: "int", nullable: false),
                    OutStockPieces = table.Column<int>(type: "int", nullable: false),
                    BalanceStock = table.Column<int>(type: "int", nullable: false),
                    BalanceStockPieces = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                name: "CrystalPurchaseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrystalPurchaseId = table.Column<int>(type: "int", nullable: false),
                    CrystalId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    PiecePerPacket = table.Column<int>(type: "int", nullable: false),
                    TotalPiece = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    CrystalPurchaseId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InstallmentNo = table.Column<int>(type: "int", nullable: false),
                    InstallmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrystalPurchaseDetails");

            migrationBuilder.DropTable(
                name: "CrystalPurchaseInstallments");

            migrationBuilder.DropTable(
                name: "CrystalStocks");

            migrationBuilder.DropTable(
                name: "CrystalPurchases");

            migrationBuilder.DropColumn(
                name: "TRN",
                table: "Suppliers");
        }
    }
}
