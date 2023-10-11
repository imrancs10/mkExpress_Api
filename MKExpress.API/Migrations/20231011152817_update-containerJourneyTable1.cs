using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class updatecontainerJourneyTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureOn",
                table: "ContainerJourneys",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArrivalAt",
                table: "ContainerJourneys",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "CheckInById",
                table: "ContainerJourneys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CheckOutById",
                table: "ContainerJourneys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContainerJourneys_CheckInById",
                table: "ContainerJourneys",
                column: "CheckInById");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerJourneys_CheckOutById",
                table: "ContainerJourneys",
                column: "CheckOutById");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerJourneys_Members_CheckInById",
                table: "ContainerJourneys",
                column: "CheckInById",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerJourneys_Members_CheckOutById",
                table: "ContainerJourneys",
                column: "CheckOutById",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContainerJourneys_Members_CheckInById",
                table: "ContainerJourneys");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerJourneys_Members_CheckOutById",
                table: "ContainerJourneys");

            migrationBuilder.DropIndex(
                name: "IX_ContainerJourneys_CheckInById",
                table: "ContainerJourneys");

            migrationBuilder.DropIndex(
                name: "IX_ContainerJourneys_CheckOutById",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "CheckInById",
                table: "ContainerJourneys");

            migrationBuilder.DropColumn(
                name: "CheckOutById",
                table: "ContainerJourneys");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureOn",
                table: "ContainerJourneys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArrivalAt",
                table: "ContainerJourneys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
