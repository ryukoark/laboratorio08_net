using Microsoft.EntityFrameworkCore;
using Lab08Robbiejude.Models;
using Lab08Robbiejude.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Leer la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddDbContext<LinqexampleContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LinqexampleContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();