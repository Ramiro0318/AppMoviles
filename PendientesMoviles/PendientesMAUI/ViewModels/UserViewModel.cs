using Android.Widget;
using PendientesMAUI.Models.DTOs;
using PendientesMAUI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PendientesMAUI.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly UserService service;

        public UserViewModel(UserService service)
        {
            this.service = service;
            GoToRegisterCommand = new Command(GoToRegister);
            GoToLoginCommand = new Command(GoToLogin);
            RegistrarCommand = new Command(Register);

        }

        
        public UsuarioRequestDTO NuevoUsuario { get; set; } = new();
        public string Errores { get; set; } = null!;
        public LoginDTO Login { get; set; } = new();

        public ICommand LoginCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand GoToRegisterCommand { get; set; }
        public ICommand GoToLoginCommand { get; set; }

        private async void GoToLogin(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void GoToRegister()
        {
            await Shell.Current.GoToAsync("RegisterView");
        }

        private async void Register(object obj)
        {
            try
            {
                var result = await service.Rgistrar(NuevoUsuario);

                if (result.Item1)
                {
                    //Toast t = Toast.MakeText("el usuario ha sido registrado exitosamente", CommunityToolkit.Maui.Core.ToastDuration.Long);

                    await Shell.Current.DisplayAlert("Registro exitoso", "Registro exitoso", "Ok");

                    GoToLogin();
                }
                else
                {
                    if (result.Item2 != null)
                    {
                        Errores = string.Join("\n", result.Item2);
                        OnPropertyChanged(Errores);
                    }
                }
            }
            catch (Exception ex)
            {
                Errores = ex.Message;
                OnPropertyChanged(nameof(Errores));
            }

        }



    }
}
