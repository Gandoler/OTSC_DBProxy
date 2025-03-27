# OTSC


## OTSC_DBProxy - сервис предоставляющий интерфейс всем остальным сервисам для работы с бд 


# Other Services

- [OTSC TelegramCongr](https://github.com/Gandoler/OTSC_TelegramCongr) — сервис для работы с поздравлениями в Telegram.
- [OTSC Registration](https://github.com/Gandoler/OTSC_Registration) — сервис регистрации пользователей.
- [OTSC TGSubscription](https://github.com/Gandoler/OTSC_TGSubscription) — сервис подписок в Telegram.
- [OTSC NeiroGen](https://github.com/Gandoler/OTSC_NeiroGen) — нейросетевой генератор контента

- [Friend CRUD Service](https://github.com/trokhin87/Friend_Crud_Service) — сервис управления друзьями.
- [Auth Service](https://github.com/trokhin87/AuthService) — сервис аутентификации пользователей.
- [Update Password Service](https://github.com/trokhin87/UpdatePasswordService) — сервис смены пароля.
- [Mail Service](https://github.com/trokhin87/Mail_Service) — сервис рассылки писем.

# start Settings

## Docker

### OTSC_DBProxy

```docker
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
    && git clone https://<TOKEN>@github.com/Gandoler/OTSC_DBProxy.git myrepo

# Переходим в каталог с проектом
WORKDIR /src/myrepo/ProxyAPILevel

# Восстанавливаем зависимости и собираем проект
RUN dotnet restore ProxyAPILevel.csproj
RUN dotnet publish ProxyAPILevel.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ с минимальным размером
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Устанавливаем переменные окружения для базы данных
ENV DB_HOST = <Your env>
ENV DB_PORT = <Your env>
ENV DB_NAME = <Your env>
ENV DB_USER = <Your env>
ENV DB_PASSWORD = <Your env>

# Запускаем приложение
ENTRYPOINT ["dotnet", "ProxyAPILevel.dll"]
```

### OTSC TelegramCongr

```docker
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
    && git clone https://<TOKEN>@github.com/Gandoler/OTSC_TelegramCongr.git myrepo

# Переходим в каталог с проектом
WORKDIR /src/myrepo/Rabotiaga

# Восстанавливаем зависимости и собираем проект
RUN dotnet restore Rabotiaga.csproj
RUN dotnet publish Rabotiaga.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ с минимальным размером
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV TGSUBS = <Your env>
ENV DbProxy = <Your env>
ENV ApiKey = <Your env>

# Запускаем приложение
ENTRYPOINT ["dotnet", "Rabotiaga.dll"]
```

### OTSC Registration

```docker
# Используем образ ASP.NET для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8082


# Используем SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Устанавливаем git и клонируем репозиторий
RUN apt-get update && apt-get install -y git \
    && git clone https://<TOKEN>@github.com/Gandoler/OTSC_Registration.git myrepo

# Переходим в каталог с проектом
WORKDIR /src/myrepo/RegistrationApi

# Восстанавливаем зависимости и собираем проект
RUN dotnet restore RegistrationApi.csproj
RUN dotnet publish RegistrationApi.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ с минимальным размером
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV DbProxy = <Your env>

# Запускаем приложение
ENTRYPOINT ["dotnet", "RegistrationApi.dll"]
```

### OTSC TGSubscription

```docker
# Используем образ ASP.NET для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8083

# Используем SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Устанавливаем git и клонируем репозиторий
RUN apt-get update && apt-get install -y git \
    && git clone https://<TOKEN>@github.com/Gandoler/OTSC_TGSubscription.git myrepo

# Переходим в каталог с проектом
WORKDIR /src/myrepo/TGSubscriptionApi

# Восстанавливаем зависимости и собираем проект
RUN dotnet restore TGSubscriptionApi.csproj
RUN dotnet publish TGSubscriptionApi.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ с минимальным размером
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV DbProxy = <Your env>
ENV Secret = <Your env>
ENV BaseUrlTelegram = <Your env>

# Запускаем приложение
ENTRYPOINT ["dotnet", "TGSubscriptionApi.dll"]
```

### OTSC NeiroGen

```docker
# Используем образ ASP.NET для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8084

# Используем SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Устанавливаем git и клонируем репозиторий
RUN apt-get update && apt-get install -y git \
    && git clone https://<TOKEN>@github.com/Gandoler/OTSC_NeiroGen.git myrepo

# Переходим в каталог с проектом
WORKDIR /src/myrepo/NeiroGenApi

# Восстанавливаем зависимости и собираем проект
RUN dotnet restore NeiroGenApi.csproj
RUN dotnet publish NeiroGenApi.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ с минимальным размером
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ApiKey = <Your env>
ENV DbProxy = <Your env>
ENV ApiUrl = <Your env>

# Запускаем приложение
ENTRYPOINT ["dotnet", "NeiroGenApi.dll"]
```


### Friend CRUD Service

```docker
# Используем образ ASP.NET для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app


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


ENV BaseUrl = <Your env>
# Запускаем приложение
ENTRYPOINT ["dotnet", "WebLevel.dll"]
```


## Docker compose

```yml
version: '3.8'

services:
  registration:
    build:
      context: .
      dockerfile: reg_dockerfile
    container_name: registstr_cont
    ports:
      - "8082:8080"
    networks:
      - glebintrenet
    environment:
      - DbProxy = <Your env>

  dbproxy:
    build:
      context: .
      dockerfile: dbproxy_dockerfile
    container_name: dbcont
    ports:
      - "8081:8080"
    networks:
      - glebintrenet
    environment:
      - DB_HOST = <Your env>
      - DB_PORT = <Your env>
      - DB_NAME = <Your env>
      - DB_USER = <Your env>
      - DB_PASSWORD = <Your env>

  tgsub:
    build:
      context: .
      dockerfile: tgsub_dockerfile
    container_name: tgsubcont
    ports:
      - "8083:8080"
    networks:
      - glebintrenet
    environment:
      - DbProxy = <Your env>
      - Secret = <Your env>
      - BaseUrlTelegram = <Your env>

  neirogen:
    build:
      context: .
      dockerfile: NeiroGen_dockerfile
    container_name: neirocont
    ports:
      - "8084:8080"
    networks:
      - glebintrenet
    environment:
      - ApiKey = <Your env>
      - DbProxy = <Your env>
      - ApiUrl = <Your env>
  congrservice:
    build:
      context: .
      dockerfile: congrservice_dockerfile
    container_name: congrserviscont
    ports:
      - "8085:8080"
    networks:
      - glebintrenet
    environment:
      - TGSUBS = <Your env>
      - DbProxy = <Your env>
      - ApiKey = <Your env>
  crudservice:
    build:
      context: .
      dockerfile: crudService_dockerfile
    container_name: crudservicecont
    ports:
      - "8086:8080"
    networks:
      - glebintrenet
    environment:
      - BaseUrl = <Your env>


networks:
  glebintrenet:
    driver: bridge

```
