FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
RUN apt-get update && apt-get install -y curl iputils-ping net-tools
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["HobbyMatchAPI/HobbyMatch.API.csproj", "HobbyMatchAPI/"]
COPY ["HobbyMatch.BL/HobbyMatch.BL.csproj", "HobbyMatch.BL/"]
COPY ["HobbyMatch.Database/HobbyMatch.Database.csproj", "HobbyMatch.Database/"]
COPY ["HobbyMatch.Domain/HobbyMatch.Domain.csproj", "HobbyMatch.Domain/"]
RUN dotnet restore "HobbyMatchAPI/HobbyMatch.API.csproj"
COPY . .
WORKDIR "/src/HobbyMatchAPI"
RUN dotnet build "HobbyMatch.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "HobbyMatch.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HobbyMatch.API.dll"]