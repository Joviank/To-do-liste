# Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
# var build = mcr.microsoft.com/dotnet/sdk:10.0();

WORKDIR /src
COPY . .
# Kopier den mappen vi står i på maskinen. Lim inn i den mappen container skal stå i, altså src-mappen

WORKDIR /src/TaskService.Api
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish
# /app/publish/TaskService.Api.dll

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "TaskService.Api.dll" ]
# dotnet TaskService.Api.dll