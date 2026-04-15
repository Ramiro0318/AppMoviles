using PendientesMAUI.ViewModels;

namespace PendientesMAUI.Views;

public partial class TodoView : ContentPage
{
    private readonly TodoViewModel _vm;

    public TodoView(TodoViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();
    }
}