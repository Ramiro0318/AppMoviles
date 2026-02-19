
using RecetasAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RecetasMAUI.Services
{
    public class RecetasService
    {
        string baseurl = "https://localhost:7027/";
        HttpClient client;
        public RecetasService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseurl)
            };
        }
        public async Task<List<CategoriaDTO>> GetCategoria()
        {

            var response = await client.GetAsync("/api/recetas/categorias");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<List<CategoriaDTO>>();
                return json ?? [];
            }
            else return [];
        }

        public async Task<List<RecetaMenuDTO>> GetMenuPorCategoria(int idCategoria)
        {
            var response = await client.GetAsync("/api/recetas/categorias/"+ idCategoria);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<List<RecetaMenuDTO>>();

                if (json != null)
                {
                    json.ForEach(item => item.IdCategoria = idCategoria);
                }

                return json ?? [];
            }
            else return [];
        }
    }
}
