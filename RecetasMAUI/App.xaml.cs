using Microsoft.Extensions.DependencyInjection;
using RecetasMAUI.Views;

namespace RecetasMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Routing.RegisterRoute("menurecetas", typeof (MenuRecetasPage));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}