using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CosmicApi.Migrations
{
    public partial class updatedatabaseagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Images",
                table: "GetCosmicSpots",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "GetCosmicSpots");
        }
    }
}
