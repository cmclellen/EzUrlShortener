# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
LABEL org.opencontainers.image.source="https://github.com/cmclellen/ezurlshortener"
USER $APP_UID
WORKDIR /app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["Ez.UrlShortener.Api/Ez.UrlShortener.Api.csproj", "Ez.UrlShortener.Api/"]
COPY ["Ez.UrlShortener.Application.Domain/Ez.UrlShortener.Domain.csproj", "Ez.UrlShortener.Application.Domain/"]
COPY ["Ez.UrlShortener.Application.Persistence/Ez.UrlShortener.Persistence.csproj", "Ez.UrlShortener.Application.Persistence/"]
COPY ["Ez.UrlShortener.Application/Ez.UrlShortener.Application.csproj", "Ez.UrlShortener.Application/"]
COPY ["Ez.UrlShortener.ServiceDefaults/Ez.UrlShortener.ServiceDefaults.csproj", "Ez.UrlShortener.ServiceDefaults/"]
RUN dotnet restore "./Ez.UrlShortener.Api/Ez.UrlShortener.Api.csproj"
COPY . .
WORKDIR "/src/Ez.UrlShortener.Api"
RUN dotnet build "./Ez.UrlShortener.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ez.UrlShortener.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080
ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ez.UrlShortener.Api.dll"]