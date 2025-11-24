using Lab08.Services;
using Microsoft.EntityFrameworkCore;
using Lab08Robbiejude.Models;
using Lab08Robbiejude.Repositories;
using Lab08Robbiejude.services;
using Lab08Robbiejude.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURACIÓN DE BASE DE DATOS (Solo una vez es necesario)
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<LinqexampleContext>(options =>
    options.UseNpgsql(connectionString));

// 2. INYECCIÓN DE DEPENDENCIAS
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILinqService, LinqService>();
builder.Services.AddScoped<IReportService, ReportService>();

// 3. SERVICIOS BÁSICOS
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ---------------- PIPELINE DE HTTP (El orden aquí es vital) ----------------

// A. SWAGGER (Siempre visible)
app.UseSwagger();
app.UseSwaggerUI();

// B. CORS (¡IMPORTANTE! Debe ir ANTES de Authorization y MapControllers)
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// C. REDIRECCIÓN HTTPS (A veces da problemas en local si no tienes certificados, pero déjalo por ahora)
app.UseHttpsRedirection();

// D. AUTORIZACIÓN
app.UseAuthorization();

// E. CONFIGURACIÓN DE PUERTO PARA RENDER (Solo en Producción)
// Esto asegura que en tu PC (Development) no intente forzar el puerto 8080
if (!app.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    app.Urls.Add($"http://*:{port}");
}

// F. MAPEO DE CONTROLADORES
app.MapControllers();

app.Run();