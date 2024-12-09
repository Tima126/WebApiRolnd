# Образ (Os) + платформа .NET 
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

# Копируем все проекты
COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]

# Восстанавливаем зависимости
RUN dotnet restore "WebApplication1/WebApplication1.csproj"

# Копируем все файлы проекта
COPY . .

FROM build AS publish
RUN dotnet publish "WebApplication1/WebApplication1.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

# Запуск приложения
ENTRYPOINT ["dotnet", "WebApplication1.dll"]