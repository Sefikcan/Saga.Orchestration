#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Shipment/Shipment.Api/Shipment.Api.csproj", "Shipment/Shipment.Api/"]
RUN dotnet restore "Shipment/Shipment.Api/Shipment.Api.csproj"
COPY . .
WORKDIR "/src/Shipment/Shipment.Api"
RUN dotnet build "Shipment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shipment.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shipment.Api.dll"]