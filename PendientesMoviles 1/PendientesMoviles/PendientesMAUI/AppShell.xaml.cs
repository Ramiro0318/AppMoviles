namespace PendientesMAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Views.AddEditView), typeof(Views.AddEditView));
        Routing.RegisterRoute(nameof(Views.RegisterView), typeof(Views.RegisterView));
        Routing.RegisterRoute(nameof(Views.TodoView), typeof(Views.TodoView));
    }
}