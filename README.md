### Friend CRUD Service

# Используем образ ASP.NET для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8085

# Используем SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Устанавливаем git и клонируем репозиторий
RUN apt-get update && apt-get install -y git \
    && git clone https://<TOKEN>@github.com/trokhin87/Friend_Crud_Service.git myrepo

# Переходим в каталог с проектом
WORKDIR /src/myrepo/WebLevel

# Восстанавливаем зависимости и собираем проект
RUN dotnet restore WebLevel.csproj
RUN dotnet publish WebLevel.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ с минимальным размером
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV BaseUrl=http://dbcont:8080

# Запускаем приложение
ENTRYPOINT ["dotnet", "WebLevel.dll"]

### Auth Service

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8086

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

RUN apt-get update && apt-get install -y git \
    && git clone https://<TOKEN>@github.com/trokhin87/AuthService.git myrepo

WORKDIR /src/myrepo/AuthAPI

RUN dotnet restore AuthAPI.csproj
RUN dotnet publish AuthAPI.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV DbProxy=http://dbcont:8080

ENTRYPOINT ["dotnet", "AuthAPI.dll"]

### Update Password Service

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8087

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

RUN apt-get update && apt-get install -y git \
    && git clone https://<TOKEN>@github.com/trokhin87/UpdatePasswordService.git myrepo

WORKDIR /src/myrepo/UpdatePasswordAPI

RUN dotnet restore UpdatePasswordAPI.csproj
RUN dotnet publish UpdatePasswordAPI.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV DbProxy=http://dbcont:8080

ENTRYPOINT ["dotnet", "UpdatePasswordAPI.dll"]

### Mail Service

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8088

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

RUN apt-get update && apt-get install -y git \
    && git clone https://<TOKEN>@github.com/trokhin87/Mail_Service.git myrepo

WORKDIR /src/myrepo/MailAPI

RUN dotnet restore MailAPI.csproj
RUN dotnet publish MailAPI.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV SmtpServer=smtp.example.com
ENV SmtpPort=587
ENV SmtpUser=user@example.com
ENV SmtpPassword=yourpassword

ENTRYPOINT ["dotnet", "MailAPI.dll"]
