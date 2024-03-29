#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Auth/Auth.API/Auth.API.csproj", "Services/Auth/Auth.API/"]
COPY ["Services/Auth/Auth.Core/Auth.Core.csproj", "Services/Auth/Auth.Core/"]
RUN dotnet restore "Services/Auth/Auth.API/Auth.API.csproj"
COPY . .
WORKDIR "/src/Services/Auth/Auth.API"
RUN dotnet build "Auth.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.API.dll"]