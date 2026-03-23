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
builder.Services.AddScoped<AlumnosService>();
//builder.Services.AddTransient<IValidator<AgregarGrupoDTO>, AgregarGrupoValidator>();
//builder.Services.AddTransient<IValidator<EditarGrupoDTO>, EditarGrupoValidator>();

builder.Services.AddAutoMapper(x =>
{

}, typeof(Program).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

var app = builder.Build();
app.MapControllers();

app.Run();
