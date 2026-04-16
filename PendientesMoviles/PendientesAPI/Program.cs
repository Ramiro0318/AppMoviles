using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PendientesAPI.DTOs;
using PendientesAPI.Mappings;
using PendientesAPI.Models.DTOs;
using PendientesAPI.Models.Entities;
using PendientesAPI.Repositories;
using PendientesAPI.Services;
using PendientesAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PendientesContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddScoped(typeof(Repository<>), typeof(Repository<>));

builder.Services.AddScoped<UsuariosService>();
builder.Services.AddScoped<PendientesService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IValidator<PendienteRequestDTO>, PendienteValidator>();
builder.Services.AddScoped<IValidator<UsuarioRequestDTO>, UserValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PendientesContext>();
    db.Database.EnsureCreated();
}

app.Run();