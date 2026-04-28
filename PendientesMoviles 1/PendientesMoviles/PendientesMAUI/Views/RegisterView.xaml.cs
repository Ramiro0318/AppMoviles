using PendientesMAUI.ViewModels;

namespace PendientesMAUI.Views;

public partial class RegisterView : ContentPage
{
	public RegisterView(UserViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}