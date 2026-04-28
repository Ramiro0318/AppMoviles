using Microsoft.Extensions.Logging;
using PendientesMAUI.Services;
using PendientesMAUI.ViewModels;
using PendientesMAUI.Views;

namespace PendientesMAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton(new HttpClient
        {

            BaseAddress = new Uri("https://localhost:44334/")
        });

        builder.Services.AddSingleton<PendienteService>();
        builder.Services.AddTransient<TodoViewModel>();
        builder.Services.AddTransient<AddEditViewModel>();
        builder.Services.AddTransient<TodoView>();
        builder.Services.AddTransient<AddEditView>();
        builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<RegisterView>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<UserViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}