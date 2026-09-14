using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TallerMecanico.Migrations
{
    /// <inheritdoc />
    public partial class AddNombreUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreUsuario",
                table: "Usuarios",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreUsuario",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_NombreUsuario",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "NombreUsuario",
                table: "Usuarios");
        }
    }
}
