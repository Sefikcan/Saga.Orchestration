#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Stock/Stock.Api/Stock.Api.csproj", "Stock/Stock.Api/"]
RUN dotnet restore "Stock/Stock.Api/Stock.Api.csproj"
COPY . .
WORKDIR "/src/Stock/Stock.Api"
RUN dotnet build "Stock.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stock.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stock.Api.dll"]