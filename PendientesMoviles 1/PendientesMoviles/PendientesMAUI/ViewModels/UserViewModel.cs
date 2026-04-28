using CommunityToolkit.Maui.Alerts;
using PendientesMAUI.Models;
using PendientesMAUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PendientesMAUI.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly UserService service;
        public UsuarioRequestDTO NuevoUsuario { get; set; } = new();

        public string Errores { get; set; } = "";

        public LoginDTO Login { get; set; } = new();


        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand GotoRegisterCommand { get; set; }
        public ICommand GotoLoginCommand { get; set; }

        
        public UserViewModel(UserService service)
        {
            this.service = service;
            GotoRegisterCommand = new Command(GotoRegister);
            GotoLoginCommand = new Command(GotoLogin);
            RegisterCommand = new Command(Register);
            LoginCommand = new Command(IniciarSesion);
        }

        private async void IniciarSesion()
        {
            if (string.IsNullOrWhiteSpace(Login.Username) || string.IsNullOrWhiteSpace(Login.Password))
            {
                await Shell.Current.DisplayAlert("Error", "Usuario y contraseña requeridos", "OK");
                return;
            }

            IsBusy = true;
            try
            {
                bool ok = await service.Login(Login);
                if (ok)
                {
                    await Shell.Current.GoToAsync("TodoView");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Usuario o contraseña incorrecta.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;

        }

        private async void Register()
        {
            try
            {
                IsBusy = true;

                var result = await service.Registrar(NuevoUsuario);

                if (result.Item1)
                {
                    //var t = Toast.Make("El usuario ha sido registrado exitosamente", CommunityToolkit.Maui.Core.ToastDuration.Long);
                    //await t.Show();
                    await Shell.Current.DisplayAlert("Registro exitoso", "El usuario ha sido registrado exitosamente", "Ok");
                    await Shell.Current.GoToAsync("..");

                }
                else
                {
                    if (result.Item2 != null)
                    {
                        Errores = string.Join("\n", result.Item2);
                        OnPropertyChanged(nameof(Errores));
                    }
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                Errores = ex.Message;
                OnPropertyChanged(nameof(Errores));
            }
        }

        private async void GotoLogin()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void GotoRegister()
        {
            await Shell.Current.GoToAsync("RegisterView");
        }

       

    }
}
