# Estágio 1: Build da aplicação
# Usa a imagem oficial do .NET 8 SDK para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EstoqueApi.csproj", "."]
RUN dotnet restore "EstoqueApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "EstoqueApi.csproj" -c Release -o /app/build

# Estágio 2: Publicação da aplicação
# Publica a aplicação para um formato pronto para execução
FROM build AS publish
RUN dotnet publish "EstoqueApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio 3: Contêiner de Produção (Runtime)
# Usa a imagem oficial e mais leve do .NET 8 Runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EstoqueApi.dll"]