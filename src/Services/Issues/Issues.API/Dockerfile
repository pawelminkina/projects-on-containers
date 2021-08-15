#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Issues/Issues.API/Issues.API.csproj", "Services/Issues/Issues.API/"]
RUN dotnet restore "Services/Issues/Issues.API/Issues.API.csproj"
COPY . .
WORKDIR "/src/Services/Issues/Issues.API"
RUN dotnet build "Issues.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Issues.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Issues.API.dll"]