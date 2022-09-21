#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pichincha.Api/Pichincha.Api.csproj", "Pichincha.Api/"]
COPY ["Pichincha.Infrastructure/Pichincha.Infrastructure.csproj", "Pichincha.Infrastructure/"]
COPY ["Pichincha.Utilities/Pichincha.Utilities.csproj", "Pichincha.Utilities/"]
COPY ["Pichincha.Domain/Pichincha.Domain.csproj", "Pichincha.Domain/"]
RUN dotnet restore "Pichincha.Api/Pichincha.Api.csproj"
COPY . .
WORKDIR "/src/Pichincha.Api"
RUN dotnet build "Pichincha.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pichincha.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pichincha.Api.dll"]