# WordsmithHub.Infrastructure

## Databases

### Docker

To mount the database volumes using the compose.yaml file with the development environment variables:

```bash
docker compose --env-file .env.dev -f compose.dev.yaml up
```

### Data

Contains the various database migration histories separately to prevent issues with the migrations and updates.

### Identity Database

1. Database managing the users and roles.
2. Uses Microsoft.AspNetCore.Identity.

#### Migrations

First, navigate to WordsmithHub.Infrastructure.

To start a new migration:

```bash
dotnet ef migrations add MigrationName --context IdentityDbContext --output-dir Data/IdentityDatabase/Migrations
```

To update the database:

```bash
dotnet ef database update --context IdentityDbContext
```

### Main Database

1. Database managing all application data.

#### Migrations

To start a new migration:

```bash
dotnet ef migrations add MigrationName --context MainDbContext --output-dir Data/MainDatabase/Migrations
```

To update the database:

```bash
dotnet ef database update --context MainDbContext
```
