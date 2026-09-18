# TallerMecanico API

API RESTful para el control básico de un taller mecánico real. Es un proyecto backend desarrollado en C# y ASP.NET Core para practicar la gestión de vehículos, órdenes de servicio y control de estados sin duplicidad de datos.

Nació de un encargo real, por lo que responde a un flujo de trabajo práctico. Únicamente se retiró el sistema de identificación interno de la empresa por privacidad.

## Tecnologías

- C# / .NET
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server 2022 (Docker)
- JWT (JSON Web Tokens)
- Swagger

## Características y Funcionalidades

- **Autenticación:** Registro y Login con JWT. Manejo de roles "Administrador y "Comun".
- **Vehículos (Unidades):** Registro, actualización y borrado lógico ("Activo = false""). Control de placas únicas.
- **Órdenes de Servicio:** Creación, cancelación y actualización automática del estado de la orden según el avance de sus servicios.
- **Paginación:** Consultas de listas paginadas usando "page" y "pageSize" con "Skip" y "Take".

## Reglas de Negocio

El sistema maneja 4 entidades principales: Usuario, Unidad, OrdenServicio y Servicio.

### Estados de la Orden
El estado de la orden depende directamente de sus servicios activos:
- Sin servicios: Pendiente
- Con servicios en ejecución: EnProceso
- Algunos servicios terminados y otros pendientes: ParcialmenteCompletada
- Todos los servicios terminados: Completada

### Transiciones de Servicios
- Pendiente -> EnProceso | Cancelado
- EnProceso -> Completado | Cancelado
- Los estados Completado o Cancelado son finales y no pueden modificarse. Una orden cancelada bloquea cambios en sus servicios

## Endpoints

### Auth
- POST /api/Auth/register
- POST /api/Auth/login
- GET /api/Auth/me

### Unidad
- GET /api/Unidad?page=1&pageSize=10
- GET /api/Unidad/{id}
- POST /api/Unidad` (Admin)
- PUT /api/Unidad/{id}` (Admin)
- DELETE /api/Unidad/{id}` (Admin - Desactivación lógica)

### OrdenServicio
- POST /api/OrdenServicio
- GET /api/OrdenServicio
- GET /api/OrdenServicio/{id}
- PATCH /api/OrdenServicio/{id}/cancelar

### Servicio
- POST /api/Servicio
- GET /api/Servicio
- GET /api/Servicio/{id}
- PATCH /api/Servicio/{id}/estado

## Paginación

Respuesta estructurada para consultas con listas

json
{
  "items": [],
  "page": 1,
  "pageSize": 10,
  "totalItems": 37,
  "totalPages": 4
}

## Configuración y Ejecución Local
Requisitos:
- .NET SDK
- Docker y SQL Server
- EF Core CLI (dotnet tool install --global dotnet-ef)

Pasos:
Clonar el repositorio y restaurar paquetes

Bash
git clone <LINK_DEL_REPOSITORIO>
cd TallerMecanico
dotnet restore
Levantamiento de base de datos en Docker

Bash
docker run --name sqlserver-tallermecanico -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=TuPassword123!" -p 1433:1433 -d [mcr.microsoft.com/mssql/server:2022-latest](https://mcr.microsoft.com/mssql/server:2022-latest)
Configurar secretos locales (User Secrets)

Bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=TallerMecanicoDb;User Id=sa;Password=TuPassword123!;TrustServerCertificate=True;"
dotnet user-secrets set "Jwt:Key" "TuClaveSecretaParaJWT"
Aplicar migraciones y ejecutar

Bash
dotnet ef database update
dotnet run
