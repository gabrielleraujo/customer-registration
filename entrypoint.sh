#!/bin/bash

# Listar os arquivos e diretórios em /app (opcional, para depuração)
echo "Conteúdo do diretório /app:"
ls /app
#ls /app/CustomerRegistration.API

# Executar a aplicação
dotnet /app/CustomerRegistration.API.dll
