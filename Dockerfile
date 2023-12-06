#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7086
EXPOSE 7087

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RCGTestBackend.API/RCGTestBackend.API.csproj", "RCGTestBackend.API/"]
COPY ["RCGTestBackend.Domain/RCGTestBackend.Domain.csproj", "RCGTestBackend.Domain/"]
COPY ["RCGTestBackend.Application/RCGTestBackend.Application.csproj", "RCGTestBackend.Application/"]
RUN dotnet restore "RCGTestBackend.API/RCGTestBackend.API.csproj"
COPY . .
WORKDIR "/src/RCGTestBackend.API"
RUN dotnet build "RCGTestBackend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RCGTestBackend.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RCGTestBackend.API.dll"]
