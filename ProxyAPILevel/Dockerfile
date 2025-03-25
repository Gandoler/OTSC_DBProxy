# Используем образ ASP.NET для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8081

# Используем SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Устанавливаем git и клонируем репозиторий
RUN apt-get update && apt-get install -y git \
    && git clone https://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@github.com/Gandoler/OTSC_DBProxy.git myrepo

# Переходим в каталог с проектом
WORKDIR /src/myrepo/ProxyAPILevel

# Восстанавливаем зависимости и собираем проект
RUN dotnet restore ProxyAPILevel.csproj
RUN dotnet publish ProxyAPILevel.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ с минимальным размером
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Запускаем приложение
ENTRYPOINT ["dotnet", "ProxyAPILevel.dll"]