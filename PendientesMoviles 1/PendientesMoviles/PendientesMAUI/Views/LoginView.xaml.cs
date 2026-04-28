using PendientesMAUI.ViewModels;

namespace PendientesMAUI.Views;

public partial class LoginView : ContentPage
{

    public LoginView(UserViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;

    }
}