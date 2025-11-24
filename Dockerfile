# 1. CAMBIO IMPORTANTE: Usamos la imagen SDK de .NET 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 2. Copiamos todo
COPY . .

# 3. Restauramos (Mantenemos la ruta corregida que ya funcionó)
RUN dotnet restore "Lab08Robbiejude/Lab08Robbiejude.csproj"

# 4. Publicamos
RUN dotnet publish "Lab08Robbiejude/Lab08Robbiejude.csproj" -c Release -o /app/publish

# 5. CAMBIO IMPORTANTE: Usamos la imagen Runtime de .NET 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# 6. Ejecutar
ENTRYPOINT ["dotnet", "Lab08Robbiejude.dll"]