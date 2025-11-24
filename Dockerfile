# 1. Imagen base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copiamos TODO (Esto incluye la carpeta Lab08Robbiejude)
COPY . .

# 3. Restauramos indicando la RUTA COMPLETA del proyecto
# (Aquí está el cambio clave: agregamos la carpeta antes del nombre del archivo)
RUN dotnet restore "Lab08Robbiejude/Lab08Robbiejude.csproj"

# 4. Publicamos indicando también la ruta completa
RUN dotnet publish "Lab08Robbiejude/Lab08Robbiejude.csproj" -c Release -o /app/publish

# 5. Imagen final para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# 6. Ejecutar
ENTRYPOINT ["dotnet", "Lab08Robbiejude.dll"]