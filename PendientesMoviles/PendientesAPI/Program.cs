using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PendientesAPI.DTOs;
using PendientesAPI.Mappings;
using PendientesAPI.Models.DTOs;
using PendientesAPI.Models.Entities;
using PendientesAPI.Repositories;
using PendientesAPI.Services;
using PendientesAPI.Validators;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x => {
        x.Audience = builder.Configuration.GetValue<string>("Jwt:Audience");
        //x.Configuration.Issuer = builder.Configuration.GetValue<string>("Jwt:Issuer");
        x.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:SecretKey")));
        x.TokenValidationParameters.ValidateAudience = true;
        x.TokenValidationParameters.ValidateIssuer = true;
        x.TokenValidationParameters.ValidateLifetime = true;
        x.TokenValidationParameters.ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience");
        x.TokenValidationParameters.ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer");
    
    });

builder.Services.AddDbContext<PendientesContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddScoped(typeof(Repository<>), typeof(Repository<>));

builder.Services.AddScoped<UsuariosService>();
builder.Services.AddScoped<PendientesService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IValidator<PendienteRequestDTO>, PendienteValidator>();
builder.Services.AddScoped<IValidator<UsuarioRequestDTO>, UserValidator>();
builder.Services.AddScoped<IValidator<LoginDTO>, LoginValidator>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PendientesContext>();
    db.Database.EnsureCreated();
}

app.Run();