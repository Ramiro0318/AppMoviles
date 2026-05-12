using GaleriaFotosApp.ViewModels;
using GaleriaFotosMaui.ViewModels;

namespace GaleriaFotosMaui.Views;

public partial class MuroPage : ContentPage
{
    private readonly FotosViewModel vm;

    public MuroPage(FotosViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
        this.vm = vm;
    }

    protected override async Task OnAppearing()
    {
        await vm.DescargarFoto();
        base.OnAppearing();

    }

}