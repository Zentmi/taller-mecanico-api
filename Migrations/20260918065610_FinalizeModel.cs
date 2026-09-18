using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TallerMecanico.Migrations
{
    /// <inheritdoc />
    public partial class FinalizeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unidades_PlacasVehiculo",
                table: "Unidades");

            migrationBuilder.AlterColumn<string>(
                name: "PrimerNombreUsuario",
                table: "Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PrimerApellidoUsuario",
                table: "Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_PlacasVehiculo",
                table: "Unidades",
                column: "PlacasVehiculo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unidades_PlacasVehiculo",
                table: "Unidades");

            migrationBuilder.AlterColumn<string>(
                name: "PrimerNombreUsuario",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PrimerApellidoUsuario",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_PlacasVehiculo",
                table: "Unidades",
                column: "PlacasVehiculo",
                unique: true,
                filter: "[Activo] = 1");
        }
    }
}
