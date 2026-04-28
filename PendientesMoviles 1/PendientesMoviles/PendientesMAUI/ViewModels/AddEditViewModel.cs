using System.Windows.Input;
using PendientesMAUI.Models;
using PendientesMAUI.Services;

namespace PendientesMAUI.ViewModels;

[QueryProperty(nameof(Pendiente), "Pendiente")]
public class AddEditViewModel : BaseViewModel
{
    private readonly PendienteService _service;

    private Pendiente? _pendiente;
    private string _titulo = string.Empty;
    private string? _descripcion;
    private string _estado = "Pendiente";

    public Pendiente? Pendiente
    {
        get => _pendiente;
        set
        {
            _pendiente = value;
            if (value != null)
            {
                Titulo = value.Titulo;
                Descripcion = value.Descripcion;
                Estado = value.Estado;
                Title = "Editar Pendiente";
            }
            else
            {
                Title = "Nuevo Pendiente";
            }
            OnPropertyChanged();
        }
    }

    public string Titulo
    {
        get => _titulo;
        set { _titulo = value; OnPropertyChanged(); }
    }

    public string? Descripcion
    {
        get => _descripcion;
        set { _descripcion = value; OnPropertyChanged(); }
    }

    public string Estado
    {
        get => _estado;
        set { _estado = value; OnPropertyChanged(); }
    }

    public List<string> EstadosDisponibles { get; } = new() { "Pendiente", "Completado" };

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public AddEditViewModel(PendienteService service)
    {
        _service = service;
        Title = "Nuevo Pendiente";
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Titulo))
        {
            await Shell.Current.DisplayAlert("Error", "El título es requerido.", "OK");
            return;
        }

        if (Titulo.Length > 100)
        {
            await Shell.Current.DisplayAlert("Error", "El título no puede exceder 100 caracteres.", "OK");
            return;
        }

        IsBusy = true;

        try
        {
            bool ok;

            if (_pendiente == null)
            {
                ok = await _service.CreateAsync(new Pendiente
                {
                    Titulo = Titulo,
                    Descripcion = Descripcion,
                    Estado = Estado
                });
            }
            else
            {
                _pendiente.Titulo = Titulo;
                _pendiente.Descripcion = Descripcion;
                _pendiente.Estado = Estado;
                ok = await _service.UpdateAsync(_pendiente);
            }

            if (ok)
                await Shell.Current.GoToAsync("..");
            else
                await Shell.Current.DisplayAlert("Error", "No se pudo guardar.", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}