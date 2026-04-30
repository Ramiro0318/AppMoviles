using System.Net.Http.Json;
using PendientesMAUI.Models;

namespace PendientesMAUI.Services;

public class PendienteService
{
    private readonly HttpClient _http;
    private readonly UserService userServce;
    private const string Endpoint = "api/pendientes";

    public PendienteService(HttpClient http, UserService userServce)
    {

        _http = http;
        this.userServce = userServce;
    }

    public async Task<List<Pendiente>> GetAllAsync()
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<Pendiente>>(Endpoint);
            return result ?? new List<Pendiente>();
        }
        catch (HttpRequestException re) when ( re.StatusCode == System.Net.HttpStatusCode.Unauthorized) 
        {
            var renovado = await userServce.refreshToken();
            if (renovado)
            {
                return await GetAllAsync();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
        catch
        {
            return new List<Pendiente>();
        }
    }

    public async Task<Pendiente?> GetByIdAsync(int id)
    {
        try
        {
            return await _http.GetFromJsonAsync<Pendiente>($"{Endpoint}/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> CreateAsync(Pendiente pendiente)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(Endpoint, new
            {
                pendiente.Titulo,
                pendiente.Descripcion,
                pendiente.Estado
            });
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Pendiente pendiente)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"{Endpoint}", new
            {
                pendiente.Id,
                pendiente.Titulo,
                pendiente.Descripcion,
                pendiente.Estado
            });
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"{Endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}