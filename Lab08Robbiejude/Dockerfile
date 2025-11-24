# Usamos .NET 8 (Tu versión)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos todo (ya que configuraste el Root Directory en Render)
COPY . .

# Restauramos y publicamos
RUN dotnet restore "Lab08Robbiejude.csproj"
RUN dotnet publish "Lab08Robbiejude.csproj" -c Release -o /app/publish

# Imagen final para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# OJO: Como agregaste el código en Program.cs, ya no necesitamos la línea ENV ASPNETCORE_URLS aquí.
# Render pasará el puerto automáticamente.

ENTRYPOINT ["dotnet", "Lab08Robbiejude.dll"]