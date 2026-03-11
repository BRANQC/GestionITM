using AutoMapper;
using GestionITM.API.Mappings;
using GestionITM.Domain.Interfaces;
using GestionITM.Infrastructure;
using GestionITM.Infrastructure.Middleware;
using GestionITM.Infrastructure.Repositories;
using GestionITM.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Configurar la cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Registrar el Repositorio (Inyección de Dependencias)
// AddScoped significa: "Crea una instancia por cada petición HTTP"
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();