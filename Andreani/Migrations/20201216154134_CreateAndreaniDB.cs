using Microsoft.EntityFrameworkCore.Migrations;

namespace Andreani.Migrations
{
    public partial class CreateAndreaniDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Geocodificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitud = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geocodificacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Geolocalizacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitud = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geolocalizacion", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Geocodificacion");

            migrationBuilder.DropTable(
                name: "Geolocalizacion");
        }
    }
}
