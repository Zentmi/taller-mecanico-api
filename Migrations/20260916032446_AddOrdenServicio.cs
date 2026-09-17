using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TallerMecanico.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdenServicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesServicio_Usuarios_UsuarioOrdenServicio",
                table: "OrdenesServicio");

            migrationBuilder.DropColumn(
                name: "ObervacionOrdenServicio",
                table: "OrdenesServicio");

            migrationBuilder.RenameColumn(
                name: "UsuarioOrdenServicio",
                table: "OrdenesServicio",
                newName: "UsuarioIdOrdenServicio");

            migrationBuilder.RenameColumn(
                name: "StatusOrdenServicio",
                table: "OrdenesServicio",
                newName: "StatusOrden");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenesServicio_UsuarioOrdenServicio",
                table: "OrdenesServicio",
                newName: "IX_OrdenesServicio_UsuarioIdOrdenServicio");

            migrationBuilder.AddColumn<string>(
                name: "ObservacionOrdenServicio",
                table: "OrdenesServicio",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesServicio_Usuarios_UsuarioIdOrdenServicio",
                table: "OrdenesServicio",
                column: "UsuarioIdOrdenServicio",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesServicio_Usuarios_UsuarioIdOrdenServicio",
                table: "OrdenesServicio");

            migrationBuilder.DropColumn(
                name: "ObservacionOrdenServicio",
                table: "OrdenesServicio");

            migrationBuilder.RenameColumn(
                name: "UsuarioIdOrdenServicio",
                table: "OrdenesServicio",
                newName: "UsuarioOrdenServicio");

            migrationBuilder.RenameColumn(
                name: "StatusOrden",
                table: "OrdenesServicio",
                newName: "StatusOrdenServicio");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenesServicio_UsuarioIdOrdenServicio",
                table: "OrdenesServicio",
                newName: "IX_OrdenesServicio_UsuarioOrdenServicio");

            migrationBuilder.AddColumn<string>(
                name: "ObervacionOrdenServicio",
                table: "OrdenesServicio",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesServicio_Usuarios_UsuarioOrdenServicio",
                table: "OrdenesServicio",
                column: "UsuarioOrdenServicio",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
