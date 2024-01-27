using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKExpress.API.Migrations
{
    /// <inheritdoc />
    public partial class Update_AssignShipmentMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "AssignShipmentMembers");

            migrationBuilder.DropColumn(
                name: "NewStatus",
                table: "AssignShipmentMembers");

            migrationBuilder.RenameColumn(
                name: "OldStatus",
                table: "AssignShipmentMembers",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AssignShipmentMembers",
                newName: "OldStatus");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "AssignShipmentMembers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NewStatus",
                table: "AssignShipmentMembers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
