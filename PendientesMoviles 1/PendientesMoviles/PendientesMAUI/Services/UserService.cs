using PendientesMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PendientesMAUI.Services
{
    public class UserService
    {
        private readonly HttpClient cliente;

        public UserService(HttpClient cliente)
        {
            this.cliente = cliente;
        }
        public async Task<(bool, List<string>?)> Registrar(UsuarioRequestDTO dto)
        {
            try
            {
                var resultado = await cliente.PostAsJsonAsync("api/usuarios", dto);

                if (resultado.IsSuccessStatusCode)
                {
                    return (true, null);
                }
                else
                {
                    var errores = await resultado.Content.ReadFromJsonAsync<List<string>>();
                    return (false, errores);
                }
            }
            catch (Exception ex)
            {
                return (false, new List<string> { ex.Message });

            }

        }

        public async Task<bool> Login(LoginDTO dto)
        {
            var response = await cliente.PostAsJsonAsync("api/auth", dto);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                //Guardarlo para uso posterior: SecureStorage

                await SecureStorage.SetAsync("MiToken", token);

                //Colocarlo como header del HTTPClient

                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                return true;
            }


            return false;
        }
    }
}
