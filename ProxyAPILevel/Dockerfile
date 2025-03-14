FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProxyAPILevel/ProxyAPILevel.csproj", "ProxyAPILevel/"]
COPY ["Infrastructure.Buisiness/Infrastructure.Buisiness.csproj", "Infrastructure.Buisiness/"]
COPY ["Domain.Core/Domain.Core.csproj", "Domain.Core/"]
COPY ["Domain.Interfaces/Domain.Interfaces.csproj", "Domain.Interfaces/"]
COPY ["Domain.DTO/Domain.DTO.csproj", "Domain.DTO/"]
COPY ["Infrastructure.DATA/Infrastructure.DATA.csproj", "Infrastructure.DATA/"]
RUN dotnet restore "ProxyAPILevel/ProxyAPILevel.csproj"
COPY . .
WORKDIR "/src/ProxyAPILevel"
RUN dotnet build "ProxyAPILevel.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProxyAPILevel.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProxyAPILevel.dll"]
