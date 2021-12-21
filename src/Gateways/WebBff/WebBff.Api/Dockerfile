#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateways/WebBff/WebBff.Api/WebBff.Api.csproj", "Gateways/WebBff/WebBff.Api/"]
RUN dotnet restore "Gateways/WebBff/WebBff.Api/WebBff.Api.csproj"
COPY . .
WORKDIR "/src/Gateways/WebBff/WebBff.Api"
RUN dotnet build "WebBff.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebBff.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebBff.Api.dll"]