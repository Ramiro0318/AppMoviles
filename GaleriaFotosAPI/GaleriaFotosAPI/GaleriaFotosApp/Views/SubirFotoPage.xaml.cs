using AndroidX.Lifecycle;
using GaleriaFotosApp.ViewModels;
using GaleriaFotosMaui.ViewModels;

namespace GaleriaFotosMaui.Views;

public partial class SubirFotoPage : ContentPage
{
    public SubirFotoPage(FotosViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}