# TallerMecanico API

API RESTful para el control básico de un taller mecánico real. Es un proyecto backend desarrollado en C# y ASP.NET Core para practicar la gestión de vehículos, órdenes de servicio y control de estados, evitando duplicidad de datos y aplicando reglas de negocio.

Nació de un encargo real, por lo que responde a un flujo de trabajo práctico. Únicamente se retiró el sistema de identificación interno de la empresa por privacidad.

## Tecnologías

- C# / .NET
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server 2022 (Docker)
- JWT (JSON Web Tokens)
- Swagger

## Características y Funcionalidades

- **Autenticación y autorización:** Registro y Login mediante JWT, con manejo de roles `Administrador` y `Comun`.
- **Vehículos (Unidades):** Registro, consulta, actualización y borrado lógico mediante el campo `Activo`. Control de placas únicas.
- **Órdenes de Servicio:** Creación, consulta y cancelación. El estado de la orden se actualiza automáticamente según el estado de sus servicios.
- **Servicios:** Creación, consulta y actualización de estado, respetando las reglas definidas para cada orden.
- **Paginación:** Consultas de listas paginadas mediante los parámetros `page` y `pageSize`, utilizando `Skip` y `Take` de Entity Framework Core.

## Reglas de Negocio

El sistema maneja 4 entidades principales: Usuario, Unidad, OrdenServicio y Servicio.

### Estados de la Orden

El estado de la orden depende directamente de sus servicios activos:

- Sin servicios: `Pendiente`
- Con servicios en ejecución: `EnProceso`
- Algunos servicios terminados y otros pendientes: `ParcialmenteCompletada`
- Todos los servicios activos terminados: `Completada`
- Orden cancelada: `Cancelada`

Una orden cancelada no puede volver a otro estado y bloquea modificaciones sobre los servicios que pertenecen a ella.

### Transiciones de Servicios

- `Pendiente` -> `EnProceso` | `Cancelado`
- `EnProceso` -> `Completado` | `Cancelado`
- Los estados `Completado` y `Cancelado` son finales y no pueden modificarse.
- Solo se pueden agregar nuevos servicios a una orden cuyo estado sea `Pendiente`.

## Endpoints

### Auth

- `POST /api/Auth/register`
- `POST /api/Auth/login`
- `GET /api/Auth/me`

### Unidad

- `GET /api/Unidad?page=1&pageSize=10`
- `GET /api/Unidad/{id}`
- `POST /api/Unidad` (Admin)
- `PUT /api/Unidad/{id}` (Admin)
- `DELETE /api/Unidad/{id}` (Admin - Desactivación lógica)

### OrdenServicio

- `POST /api/OrdenServicio`
- `GET /api/OrdenServicio`
- `GET /api/OrdenServicio/{id}`
- `PATCH /api/OrdenServicio/{id}/cancelar`

### Servicio

- `POST /api/Servicio`
- `GET /api/Servicio`
- `GET /api/Servicio/{id}`
- `PATCH /api/Servicio/{id}/estado`

## Paginación

Las consultas de listas pueden utilizar paginación mediante los parámetros `page` y `pageSize`.

La respuesta contiene la información de la página solicitada, el tamaño de página y el total de registros disponibles.

```json
{
  "items": [],
  "page": 1,
  "pageSize": 10,
  "totalItems": 37,
  "totalPages": 4
}

Configuración y Ejecución Local

Requisitos:

.NET SDK
Docker
SQL Server 2022
EF Core CLI (dotnet tool install --global dotnet-ef)

Pasos:

Clonar el repositorio y restaurar paquetes.

git clone https://github.com/Zentmi/taller-mecanico-api.git
cd taller-mecanico-api
dotnet restore

Levantamiento de la base de datos en Docker.

docker run --name sqlserver-tallermecanico \
  -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=TuPassword123!" \
  -p 1433:1433 \
  -d mcr.microsoft.com/mssql/server:2022-latest

El valor TuPassword123! es únicamente un ejemplo. Debe sustituirse por una contraseña que cumpla los requisitos de SQL Server.

Configurar secretos locales mediante User Secrets.

dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=TallerMecanicoDb;User Id=sa;Password=TuPassword123!;TrustServerCertificate=True;"
dotnet user-secrets set "Jwt:Key" "TuClaveSecretaParaJWT"

Aplicar las migraciones y ejecutar la aplicación.

dotnet ef database update
dotnet run

La documentación de los endpoints puede consultarse mediante Swagger una vez iniciada la aplicación.

