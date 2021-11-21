# Base ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
LABEL org.opencontainers.image.source="https://github.com/aydinfurkan/user-service"
EXPOSE 5000

# Build layer
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

ARG NUGET_CR_PAT
COPY . .
RUN rm nuget.config
COPY ./Nuget.Config.ci ./Nuget.Config
RUN sed -i -e "s/NUGET_CR_PAT/$NUGET_CR_PAT/g" nuget.config
RUN dotnet restore

WORKDIR /UserService
RUN dotnet build UserService.csproj -c Release -o /app

# Publish dll
FROM build AS publish
RUN dotnet publish UserService.csproj -c Release -o /app 

# Entrypoint
FROM base AS final
COPY --from=publish /app .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "UserService.dll"]
