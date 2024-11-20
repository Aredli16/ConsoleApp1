FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Leonardo.Web/Leonardo.Web.csproj", "src/Leonardo.Web/"]
COPY ["src/Leonardo/Leonardo.csproj", "src/Leonardo/"]
RUN dotnet restore "src/Leonardo.Web/Leonardo.Web.csproj"
COPY . .
WORKDIR "/src/src/Leonardo.Web"
RUN dotnet build "Leonardo.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Leonardo.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Leonardo.Web.dll"]
