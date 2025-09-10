# Est�gio 1: Build da aplica��o
# Usa a imagem oficial do .NET 8 SDK para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EstoqueApi.csproj", "."]
RUN dotnet restore "EstoqueApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "EstoqueApi.csproj" -c Release -o /app/build

# Est�gio 2: Publica��o da aplica��o
# Publica a aplica��o para um formato pronto para execu��o
FROM build AS publish
RUN dotnet publish "EstoqueApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Est�gio 3: Cont�iner de Produ��o (Runtime)
# Usa a imagem oficial e mais leve do .NET 8 Runtime para rodar a aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EstoqueApi.dll"]