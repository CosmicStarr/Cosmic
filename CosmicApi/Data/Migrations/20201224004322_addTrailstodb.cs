using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CosmicApi.Migrations
{
    public partial class addTrailstodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetDirections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    Difficulty = table.Column<int>(nullable: false),
                    CosmicSpotId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetDirections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GetDirections_GetCosmicSpots_CosmicSpotId",
                        column: x => x.CosmicSpotId,
                        principalTable: "GetCosmicSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GetDirections_CosmicSpotId",
                table: "GetDirections",
                column: "CosmicSpotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetDirections");
        }
    }
}
