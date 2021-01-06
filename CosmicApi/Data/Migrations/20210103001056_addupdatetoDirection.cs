using Microsoft.EntityFrameworkCore.Migrations;

namespace CosmicApi.Migrations
{
    public partial class addupdatetoDirection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Elevation",
                table: "GetDirections",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Elevation",
                table: "GetDirections");
        }
    }
}
