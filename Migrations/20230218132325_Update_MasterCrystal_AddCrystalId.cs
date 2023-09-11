﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MKExpress.Web.API.Migrations
{
    public partial class Update_MasterCrystal_AddCrystalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CrystalId",
                table: "MasterCrystals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CrystalId",
                table: "MasterCrystals");
        }
    }
}
