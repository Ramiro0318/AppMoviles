using Android.Webkit;
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
    }
}
