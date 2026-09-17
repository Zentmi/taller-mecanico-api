using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TallerMecanico.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    IdVehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarcaVehiculo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ModeloVehiculo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ColorVehiculo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CombustibleVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnioVehiculo = table.Column<int>(type: "int", nullable: false),
                    PlacasVehiculo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FechaRegistroVehiculo = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.IdVehiculo);
                    table.CheckConstraint("CK_Unidad_Anio", "[AnioVehiculo] >= 1900 AND [AnioVehiculo] <= 2100");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PrimerNombreUsuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrimerApellidoUsuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolUsuario = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesServicio",
                columns: table => new
                {
                    PkOrden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrdenServicio = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaOrdenSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    StatusOrden = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UsuarioIdOrdenServicio = table.Column<int>(type: "int", nullable: false),
                    ObservacionOrdenServicio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IdVehiculo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesServicio", x => x.PkOrden);
                    table.ForeignKey(
                        name: "FK_OrdenesServicio_Unidades_IdVehiculo",
                        column: x => x.IdVehiculo,
                        principalTable: "Unidades",
                        principalColumn: "IdVehiculo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenesServicio_Usuarios_UsuarioIdOrdenServicio",
                        column: x => x.UsuarioIdOrdenServicio,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    PkServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdServicio = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FkOrden = table.Column<int>(type: "int", nullable: false),
                    TipoServicio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DetalleServicio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CostoServicio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FechaInicioServicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinServicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCancelacionServicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusServicio = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.PkServicio);
                    table.CheckConstraint("CK_Servicio_Costo", "[CostoServicio] >= 0");
                    table.ForeignKey(
                        name: "FK_Servicios_OrdenesServicio_FkOrden",
                        column: x => x.FkOrden,
                        principalTable: "OrdenesServicio",
                        principalColumn: "PkOrden",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesServicio_IdOrdenServicio",
                table: "OrdenesServicio",
                column: "IdOrdenServicio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesServicio_IdVehiculo",
                table: "OrdenesServicio",
                column: "IdVehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesServicio_UsuarioIdOrdenServicio",
                table: "OrdenesServicio",
                column: "UsuarioIdOrdenServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_FkOrden",
                table: "Servicios",
                column: "FkOrden");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_IdServicio",
                table: "Servicios",
                column: "IdServicio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_PlacasVehiculo",
                table: "Unidades",
                column: "PlacasVehiculo",
                unique: true,
                filter: "[Activo] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreUsuario",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "OrdenesServicio");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
