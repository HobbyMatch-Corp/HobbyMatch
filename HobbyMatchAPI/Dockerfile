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

RUN mkdir -p /app/certificates
COPY HobbyMatchAPI/certificates/aspnetapp.pfx /app/certificates

ENTRYPOINT ["dotnet", "HobbyMatch.API.dll"]

## See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
## This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#USER root
#RUN apt-get update && apt-get install -y curl iputils-ping net-tools
#USER $APP_UID
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["HobbyMatchAPI/HobbyMatch.API.csproj", "HobbyMatchAPI/"]
#COPY ["HobbyMatch.BL/HobbyMatch.BL.csproj", "HobbyMatch.BL/"]
#COPY ["HobbyMatch.Database/HobbyMatch.Database.csproj", "HobbyMatch.Database/"]
#COPY ["HobbyMatch.Domain/HobbyMatch.Domain.csproj", "HobbyMatch.Domain/"]
#RUN dotnet restore "./HobbyMatchAPI/HobbyMatch.API.csproj"
#COPY . .
#WORKDIR "/src/HobbyMatchAPI"
#RUN dotnet build "./HobbyMatch.API.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./HobbyMatch.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "HobbyMatch.API.dll"]