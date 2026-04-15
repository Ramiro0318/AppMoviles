using PendientesMAUI.ViewModels;

namespace PendientesMAUI.Views;

public partial class AddEditView : ContentPage
{
    public AddEditView(AddEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}