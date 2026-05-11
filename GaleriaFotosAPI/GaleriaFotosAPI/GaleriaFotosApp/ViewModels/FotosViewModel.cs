using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GaleriaFotosApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleriaFotosApp.ViewModels
{
    public partial class FotosViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isLoading;
        [ObservableProperty]
        private string mensaje;
        [ObservableProperty]
        private string? rutaImagen = "";
        private readonly FotoService service;


        [RelayCommand]
        public async Task TomarFoto()
        {
            Mensaje = "";
            var ruta = await service.TomarFoto();

            if (ruta == null)
            {
                Mensaje = "No se ha podido tomar foto";
            }
            else
            {
                RutaImagen = ruta;
            }
        }

        [RelayCommand]
        public async Task SubirFoto()
        {
            try
            {

                if (RutaImagen != null)
                {
                    IsLoading = true;
                    Mensaje = "Carnaod foto...";
                    var id = await service.SubirFoto(RutaImagen);
                    if (id == null)
                    {
                        Mensaje = "No se pudo cargar la foto";
                    }
                    else
                    {
                        Mensaje = "Fotografía tomada exitosamente.";
                        RutaImagen = null;
                    }
                    IsLoading = false;
                }
                else
                {
                    Mensaje = "Tome o seleccione una foto antes de subirla";
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            finally { IsLoading = false; }
        }


        public FotosViewModel(FotoService service)
        {
            this.service = service;
        }



    }
}
