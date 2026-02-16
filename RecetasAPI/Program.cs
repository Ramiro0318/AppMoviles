using RecetasAPI.Data;
using RecetasAPI.Repositories;
using RecetasAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


//Registrar los sericios como inversión de dependencias (DI)
builder.Services.AddDbContext<GourmetRecetasContext>();
builder.Services.AddScoped<RecetasService>();
builder.Services.AddScoped<CategoriasService>();
builder.Services.AddScoped(typeof(Repository<>), typeof(Repository<>));

var app = builder.Build();
app.MapControllers();

app.Run();
