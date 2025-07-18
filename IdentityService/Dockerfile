﻿ 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081 

 
HEALTHCHECK --interval=30s --timeout=10s --retries=3 \
  CMD curl -f http://localhost/health || exit 1

 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

 
COPY ["IdentityService/IdentityService.csproj", "IdentityService/"]
RUN dotnet restore "IdentityService/IdentityService.csproj"

 
COPY . .
WORKDIR "/src/IdentityService"
RUN dotnet build "IdentityService.csproj" -c $BUILD_CONFIGURATION -o /app/build

 
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "IdentityService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.dll"]
