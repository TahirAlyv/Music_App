 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

 
COPY ["GateWay/GateWay.csproj", "GateWay/"]
RUN dotnet restore "GateWay/GateWay.csproj"

 
COPY . .
WORKDIR "/src/GateWay"
RUN dotnet build "GateWay.csproj" -c $BUILD_CONFIGURATION -o /app/build

 
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GateWay.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY GateWay/ocelot.json /app/publish/ocelot.json

ENTRYPOINT ["dotnet", "GateWay.dll"]
