# Base ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
LABEL org.opencontainers.image.source="https://github.com/aydinfurkan/user-service"
EXPOSE 5000

# Build layer
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY . .
WORKDIR /UserService.Application
RUN dotnet build UserService.Application.csproj -c Release -o /app

# Publish dll
FROM build AS publish
RUN dotnet publish UserService.Application.csproj -c Release -o /app 

# Entrypoint
FROM base AS final
COPY --from=publish /app .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "UserService.Application.dll"]
