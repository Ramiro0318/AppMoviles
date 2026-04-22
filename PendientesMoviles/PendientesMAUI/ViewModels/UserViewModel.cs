using PendientesMAUI.Models.DTOs;
using PendientesMAUI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PendientesMAUI.ViewModels
{
    public class UserViewModel
    {
        private readonly UserService service;

        public UserViewModel(UserService service)
        {
            this.service = service;
            GoToRegisterCommand = new Command(GoToRegister);
            GoToLoginCommand = new Command(GoToLogin);

        }

      

        public UsuarioRequestDTO NuevoUsuario { get; set; } = new();
        public LoginDTO Login { get; set; } = new();

        public ICommand LoginCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand GoToRegisterCommand { get; set; }
        public ICommand GoToLoginCommand{ get; set; }

        private async void GoToLogin(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void GoToRegister()
        {
            await Shell.Current.GoToAsync("RegisterView");
        }



    }
}
