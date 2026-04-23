FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

USER root
RUN apt-get update && apt-get install -y --no-install-recommends \
    libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*

ARG APP_UID=1000
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WordsmithHub.API/WordsmithHub.API.csproj", "WordsmithHub.API/"]
COPY ["WordsmithHub.Domain/WordsmithHub.Domain.csproj", "WordsmithHub.Domain/"]
COPY ["WordsmithHub.Infrastructure/WordsmithHub.Infrastructure.csproj", "WordsmithHub.Infrastructure/"]
RUN dotnet restore "WordsmithHub.API/WordsmithHub.API.csproj"
COPY . .
WORKDIR "/src/WordsmithHub.API"
RUN dotnet build "./WordsmithHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WordsmithHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WordsmithHub.API.dll"]
