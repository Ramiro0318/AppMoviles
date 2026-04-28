using System.Collections.ObjectModel;
using System.Windows.Input;
using PendientesMAUI.Models;
using PendientesMAUI.Services;

namespace PendientesMAUI.ViewModels;

public class TodoViewModel : BaseViewModel
{
    private readonly PendienteService _service;

    public ObservableCollection<Pendiente> Pendientes { get; } = new();
    public string Usuario { get; set; } = "";

    public ICommand LoadCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand ToggleEstadoCommand { get; }
    public ICommand GoToAddCommand { get; }
    public ICommand GoToEditCommand { get; }

    public TodoViewModel(PendienteService service)
    {
        _service = service;
        Title = "Mis Pendientes";

        LoadCommand = new Command(async () => await LoadAsync());
        DeleteCommand = new Command<Pendiente>(async (p) => await DeleteAsync(p));
        ToggleEstadoCommand = new Command<Pendiente>(async (p) => await ToggleEstadoAsync(p));
        GoToAddCommand = new Command(async () => await GoToAddAsync());
        GoToEditCommand = new Command<Pendiente>(async (p) => await GoToEditAsync(p));
    }

    public async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            var token = await SecureStorage.GetAsync("MiToken");

            if (!string.IsNullOrEmpty(token))
            {
                var nombre = JwtHelper.GetName(token);
                Usuario = !string.IsNullOrEmpty(nombre) ? nombre : "Usuario";
            }
            else
            {
                Usuario = "Desconocido";
            }

            OnPropertyChanged(nameof(Usuario));

            var items = await _service.GetAllAsync();
            Pendientes.Clear();
            foreach (var item in items)
                Pendientes.Add(item);
        }
        catch
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo cargar la lista.", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task DeleteAsync(Pendiente pendiente)
    {
        bool confirm = await Shell.Current.DisplayAlert(
            "Eliminar",
            $"¿Eliminar '{pendiente.Titulo}'?",
            "Sí", "Cancelar");

        if (!confirm) return;

        var ok = await _service.DeleteAsync(pendiente.Id);
        if (ok)
            Pendientes.Remove(pendiente);
        else
            await Shell.Current.DisplayAlert("Error", "No se pudo eliminar.", "OK");
    }

    private async Task ToggleEstadoAsync(Pendiente pendiente)
    {
        pendiente.Estado = pendiente.Estado == "Completado" ? "Pendiente" : "Completado";
        var ok = await _service.UpdateAsync(pendiente);
        if (ok)
        {
            var index = Pendientes.IndexOf(pendiente);
            if (index >= 0)
            {
                Pendientes.RemoveAt(index);
                Pendientes.Insert(index, pendiente);
            }
        }
        else
        {
            pendiente.Estado = pendiente.Estado == "Completado" ? "Pendiente" : "Completado";
            await Shell.Current.DisplayAlert("Error", "No se pudo actualizar.", "OK");
        }
    }

    private async Task GoToAddAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.AddEditView));
    }

    private async Task GoToEditAsync(Pendiente pendiente)
    {
        await Shell.Current.GoToAsync(
            nameof(Views.AddEditView),
            new Dictionary<string, object> { ["Pendiente"] = pendiente });
    }
}