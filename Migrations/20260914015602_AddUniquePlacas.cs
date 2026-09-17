using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TallerMecanico.Migrations
{
    /// <inheritdoc />
    public partial class AddUniquePlacas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Unidades_PlacasVehiculo",
                table: "Unidades",
                column: "PlacasVehiculo",
                unique: true,
                filter: "[Activo] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unidades_PlacasVehiculo",
                table: "Unidades");
        }
    }
}
