# Use a imagem base do .NET 7
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use a imagem base do .NET 7 para compilação
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CustomerRegistration.API/CustomerRegistration.API.csproj", "CustomerRegistration.API/"]
RUN dotnet restore "CustomerRegistration.API/CustomerRegistration.API.csproj"
COPY . .
WORKDIR "/src/CustomerRegistration.API"
RUN dotnet build "CustomerRegistration.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerRegistration.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerRegistration.API.dll"]
