FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EShop.Presentation/EShop.Presentation.csproj", "EShop.Presentation/"]
RUN dotnet restore "EShop.Presentation/EShop.Presentation.csproj"
COPY . .
WORKDIR "/src/EShop.Presentation"
RUN dotnet build "EShop.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EShop.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Change ownership of wwwroot directory
USER root
RUN chown -R $APP_UID:$APP_UID /app/wwwroot

USER $APP_UID
ENTRYPOINT ["dotnet", "EShop.Presentation.dll"]
