# ── Estágio 1: Build ────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia apenas o .csproj primeiro (cache de restore)
COPY TarefasApi.csproj .
RUN dotnet restore

# Copia o resto e publica
COPY . .
RUN dotnet publish -c Release -o /app/publish

# ── Estágio 2: Runtime (imagem final pequena) ───────────
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Porta que a API vai escutar
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TarefasApi.dll"]