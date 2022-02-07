#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Users/Users.API/Users.API.csproj", "Services/Users/Users.API/"]
COPY ["BuildingBlocks/WebHost/WebHost.Customization/WebHost.Customization.csproj", "BuildingBlocks/WebHost/WebHost.Customization/"]
COPY ["Services/Users/Users.DAL/Users.DAL.csproj", "Services/Users/Users.DAL/"]
COPY ["Services/Users/Users.Core/Users.Core.csproj", "Services/Users/Users.Core/"]
COPY ["Architecture/Architecture.DDD/Architecture.DDD.csproj", "Architecture/Architecture.DDD/"]
COPY ["BuildingBlocks/EventBus/EventBus.csproj", "BuildingBlocks/EventBus/"]
RUN dotnet restore "Services/Users/Users.API/Users.API.csproj"
COPY . .
WORKDIR "/src/Services/Users/Users.API"
RUN dotnet build "Users.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Users.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.API.dll"]