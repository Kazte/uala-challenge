# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ualax.api/ualax.api.csproj", "ualax.api/"]
COPY ["ualax.application/ualax.application.csproj", "ualax.application/"]
COPY ["ualax.domain/ualax.domain.csproj", "ualax.domain/"]
COPY ["ualax.infrstructure/ualax.infrstructure.csproj", "ualax.infrstructure/"]
RUN dotnet restore "./ualax.api/ualax.api.csproj"
COPY . .
WORKDIR "/src/ualax.api"
RUN dotnet build "./ualax.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ualax.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ualax.api.dll"]