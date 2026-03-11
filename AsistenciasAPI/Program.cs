using AsistenciasAPI.Models.DTOs;
using AsistenciasAPI.Models.Entities;
using AsistenciasAPI.Repositories;
using AsistenciasAPI.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<RegistroasistenciaContext>(x => x.UseMySql(cs, ServerVersion.AutoDetect(cs)));
builder.Services.AddScoped(typeof(Repository<>), typeof(Repository<>));
builder.Services.AddScoped<GruposService>();
builder.Services.AddTransient<IValidator<AgregarGrupoDTO>, AgregarGrupoDTOValidator>();
builder.Services.AddTransient<IValidator<EditarGrupoDTO>, EditarGrupoDTOValidator>();

builder.Services.AddAutoMapper(x => 
{

}, typeof(Program).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();
app.MapControllers();

app.Run();
