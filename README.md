# Vitrina

Simple setup guide for running the project locally.

## What is included

- `frontend/` - Angular application
- `backend/` - .NET 10 API
- `docker-compose.yml` - starts frontend, backend, and SQL Server together

## Quick start with Docker

### Prerequisites

- Docker installed and running
- Git installed

### Steps

1. Clone the repository.
2. Open a terminal in the repository root.
3. Optional: copy `.env.example` to `.env` if you want to override the default values.
4. Run:

```sh
docker compose up --build -d
```

### Open the app

- Frontend: http://localhost:4200
- API: http://localhost:5188
- Swagger: http://localhost:5188/swagger

### Stop the app

```sh
docker compose down
```

More Docker details are available in [docs/docker-setup.md](docs/docker-setup.md).

## Run without Docker

### Prerequisites

- .NET SDK 10
- Node.js 22+
- npm 10+
- SQL Server

### 1. Configure the database

Update the connection string in `backend/src/1. Vitrina.Api/appsettings.json` or use environment variables.

### 2. Run the backend

From `backend/`:

```sh
dotnet run --project "src/1. Vitrina.Api/Vitrina.Api.csproj"
```

The API will be available at:

- http://localhost:5188
- http://localhost:5188/swagger

### 3. Run the frontend

From `frontend/`:

```sh
npm install
npm start
```

The frontend will be available at:

- http://localhost:4200

## Demo data and credentials

The database includes seeded demo data when the app starts for the first time.

### Demo users

- Admin
  - Username: `admin`
  - Password: `Admin123!`
- Regular user
  - Username: `user`
  - Password: `User123!`

### Demo data

- Sample products are already seeded in the database.

## Notes

- The backend runs Entity Framework migrations automatically on startup.
- If you use Docker and the database takes longer to start, run `docker compose restart api` once.
