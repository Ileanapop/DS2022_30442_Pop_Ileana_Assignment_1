#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["energy-utility-platform-api/energy-utility-platform-api.csproj", "energy-utility-platform-api/"]
RUN dotnet restore "energy-utility-platform-api/energy-utility-platform-api.csproj"
COPY . .
WORKDIR "/src/energy-utility-platform-api"
RUN dotnet build "energy-utility-platform-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "energy-utility-platform-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "energy-utility-platform-api.dll"]