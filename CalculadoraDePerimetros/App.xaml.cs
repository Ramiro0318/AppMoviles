using CalculadoraDePerimetros.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace CalculadoraDePerimetros
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Routing.RegisterRoute("cuadrilatero", typeof(CuadrilateroPage));
            Routing.RegisterRoute("triangulo", typeof(TrianguloPage));
            Routing.RegisterRoute("circulo", typeof(CirculoPage));
            Routing.RegisterRoute("resultados", typeof(ResultadosPage));

            return new Window(new AppShell());
        }
    }
}