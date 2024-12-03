# Selecionar imagem base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Usar imagem base do .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Definir diretório de trabalho
WORKDIR /app

# Copiar solução e arquivos de projeto
COPY ProdutoGW.sln ./

# Copiar projetos individuais para o diretório de trabalho
COPY ProdutoGW.API/ProdutoGW.API.csproj ./ProdutoGW.API/
COPY ProdutoGW.Application/ProdutoGW.Application.csproj ./ProdutoGW.Application/
COPY ProdutoGW.Domain/ProdutoGW.Domain.csproj ./ProdutoGW.Domain/
COPY ProdutoGW.Infrastructure/ProdutoGW.Infrastructure.csproj ./ProdutoGW.Infrastructure/
COPY ProdutoGW.IntegrationTests/ProdutoGW.IntegrationTests.csproj ./ProdutoGW.IntegrationTests/
COPY ProdutoGW.Tests/ProdutoGW.Tests.csproj ./ProdutoGW.Tests/

# Restaurar dependências
RUN dotnet restore

# Copiar todo o código restante
COPY . .

# Publicar a aplicação
RUN dotnet publish ProdutoGW.API/ProdutoGW.API.csproj -c Release -o out

# Definir a imagem final para o container
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "ProdutoGW.API.dll"]

