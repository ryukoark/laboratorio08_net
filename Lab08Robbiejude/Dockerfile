# 1. Usar la imagen base de .NET 8 SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# 2. Copiar los archivos del proyecto
COPY . .

# 3. Restaurar dependencias y publicar
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# 4. Usar la imagen base de ASP.NET para ejecutar (más ligera)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# 5. Configurar el puerto (Render usa este puerto internamente)
ENV ASPNETCORE_URLS=http://+:8080

# 6. Ejecutar la aplicación (Asegúrate que el nombre del DLL sea correcto)
ENTRYPOINT ["dotnet", "Lab08Robbiejude.dll"]