# ---- Etapa base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
# Porta exposta para o ambiente de desenvolvimento 
EXPOSE 32768
# Configurar a URL de ambiente para depuração
ENV ASPNETCORE_URLS=http://+:32768
# Variável específica para ambiente de desenvolvimento
ENV ASPNETCORE_ENVIRONMENT=Development

# ---- Etapa de build
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Defina a configuração para Debug
ARG configuration=Debug
WORKDIR /src
COPY ["CustomerRegistration.API/CustomerRegistration.API.csproj", "CustomerRegistration.API/"]
RUN dotnet restore "CustomerRegistration.API/CustomerRegistration.API.csproj"
COPY . .
WORKDIR "/src/CustomerRegistration.API"
RUN dotnet build "CustomerRegistration.API.csproj" -c $configuration -o /app/build

# ---- Etapa de publicação
FROM build AS publish
# Usar configuração Debug no momento da publicação
ARG configuration=Debug
RUN dotnet publish "CustomerRegistration.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false 

# ---- Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 
# Adiciona o entrypoint e define permissões
COPY ./entrypoint.sh ./ 
USER root
RUN chmod +x ./entrypoint.sh

# -- Instalação do depurador
RUN apt-get update && \
    apt-get install -y unzip && \
    curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg && \
    # Adiciona esta linha para garantir que vsdbg é executável
    chmod +x /vsdbg/vsdbg

# -- Mudar para o usuário 'app' após a configuração
USER app

# ---- Executar o entrypoint.sh
ENTRYPOINT ["./entrypoint.sh"]
