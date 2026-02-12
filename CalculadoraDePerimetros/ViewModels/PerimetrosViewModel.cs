
using CalculadoraDePerimetros.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculadoraDePerimetros.ViewModels
{
    public class PerimetrosViewModel : INotifyPropertyChanged
    {
        public Cuadrilatero Cuadrilatero { set; get; } = new();
        public string MensajeError { set; get; } = "";
        public string Resultado { set; get; } = "";
        public string Perimetro { set; get; } = "";
        public ICommand SeleccionarFiguraCommand { set; get; }
        public ICommand CalcularCommand { set; get; }
        public ICommand IrInicioCommand { set; get; }

        public PerimetrosViewModel()
        {
            SeleccionarFiguraCommand = new Command<string>(SeleccionarFigura);
            CalcularCommand = new Command<string>(Calcular);
            IrInicioCommand = new Command(IrInicio);
        }

        private void IrInicio(object obj)
        {
            Shell.Current.GoToAsync("//MainPage");
        }

        private async void SeleccionarFigura(string figura)
        {
            switch (figura)
            {
                case "cuadrilatero":
                    await Shell.Current.GoToAsync("/cuadrilatero");
                    break;
                case "triangulo":
                    await Shell.Current.GoToAsync("/triangulo"); 
                    break;
                case "circulo":
                    await Shell.Current.GoToAsync("/circulo");
                    break;
            }
        }

        private async void Calcular(string figura)
        {
            MensajeError = "";
            if (figura == "cuadrilatero")
            {
                if (Cuadrilatero.Lado1 <= 0 || Cuadrilatero.Lado2 <= 0)
                {
                    MensajeError = "Los lados de un cuadrilatero no puedes ser cero o negativos.";
                    PropertyChanged?.Invoke(this, new(nameof(MensajeError)));
                }
                else
                {

                    Cuadrilatero.Perimetro = Cuadrilatero.Lado1 * 2 + Cuadrilatero.Lado2 * 2;

                    Resultado = $"El perímetro de jun cuadrilatero de {Cuadrilatero.Lado1: 0.00} x {Cuadrilatero.Lado2: 0.00} es:";
                    Perimetro = Cuadrilatero.Perimetro.ToString("0.00");

                    await Shell.Current.GoToAsync("resultados");
                }
            }
            PropertyChanged?.Invoke(this, new(nameof(Resultado)));
            PropertyChanged?.Invoke(this, new(nameof(Perimetro)));
            PropertyChanged?.Invoke(this, new(nameof(MensajeError)));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
