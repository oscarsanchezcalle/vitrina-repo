# Docker setup for Vitrina

This repository is prepared so you can start the full application with Docker using a single command.

## Prerequisites

- Docker Desktop or Docker Engine with Docker Compose support installed
- Git installed
- Enough RAM for Docker to run SQL Server comfortably (4 GB minimum recommended for Docker)

## What will run

`docker compose` starts these containers:

- `vitrina-frontend` - Angular app served by Nginx
- `vitrina-api` - .NET 10 backend API
- `vitrina-sqlserver` - SQL Server 2022 database

## Initial setup

1. Start Docker and verify it is running.
2. Clone this repository.
3. Open a terminal in the repository root.
4. Optional but recommended: create your own local environment file.

```powershell
cp .env.example .env
```

If you do not create `.env`, Docker Compose will still run using the defaults defined in `docker-compose.yml`.

## Start everything

From the repository root, run:

```powershell
docker compose up --build -d
```

## Open the app

- Frontend: http://localhost:4200
- Backend API: http://localhost:5188
- Swagger UI: http://localhost:5188/swagger

The backend applies Entity Framework migrations automatically when it starts.

## Useful commands

See container status:

```powershell
docker compose ps
```

See logs:

```powershell
docker compose logs -f
```

Stop everything:

```powershell
docker compose down
```

Stop everything and remove the database volume:

```powershell
docker compose down -v
```

## Platform note

The SQL Server container is configured with `linux/amd64`. In environments where that image does not run natively, Docker may use emulation, so the database container can start more slowly than the other services.

If the API fails on the very first startup because SQL Server is still initializing, run this once after a few seconds:

```powershell
docker compose restart api
```

## Local configuration

You can override these values in `.env`:

- `VITRINA_DB_NAME`
- `VITRINA_DB_SA_PASSWORD`
- `VITRINA_JWT_ISSUER`
- `VITRINA_JWT_AUDIENCE`
- `VITRINA_JWT_KEY`
- `VITRINA_JWT_EXPIRATION_MINUTES`

## Next Azure step

Later, this same structure can be published to Azure by pushing the backend and frontend images to a registry and deploying them to Azure App Service, Azure Container Apps, or AKS, while moving the database to Azure SQL Database.
