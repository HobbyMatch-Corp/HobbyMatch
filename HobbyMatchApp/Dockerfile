FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
RUN apt-get update && apt-get install -y curl iputils-ping net-tools
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["HobbyMatchApp/HobbyMatch.App.csproj", "HobbyMatchApp/"]
RUN dotnet restore "HobbyMatchApp/HobbyMatch.App.csproj"
COPY . .
WORKDIR "/src/HobbyMatchApp"
RUN dotnet build "HobbyMatch.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "HobbyMatch.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HobbyMatch.App.dll"]