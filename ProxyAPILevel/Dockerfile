FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["APPLevel/ProxyAPILevel/ProxyAPILevel.csproj", "ProxyAPILevel/"]
COPY ["Infrastructure/Infrastructure.Buisiness/Infrastructure.Buisiness.csproj", "Infrastructure.Buisiness/"]
COPY ["Domain/Domain.Core/Domain.Core.csproj", "Domain.Core/"]
COPY ["Domain/Domain.Interfaces/Domain.Interfaces.csproj", "Domain.Interfaces/"]
COPY ["Domain/Domain.DTO/Domain.DTO.csproj", "Domain.DTO/"]
COPY ["Infrastructure/Infrastructure.DATA/Infrastructure.DATA.csproj", "Infrastructure.DATA/"]
RUN dotnet restore "APPLevel/ProxyAPILevel/ProxyAPILevel.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "APPLevel/ProxyAPILevel.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "APPLevel/ProxyAPILevel.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProxyAPILevel.dll"]
