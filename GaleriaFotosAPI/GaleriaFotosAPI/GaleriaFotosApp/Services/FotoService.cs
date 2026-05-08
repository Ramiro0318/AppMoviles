using GaleriaFotosApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GaleriaFotosApp.Services
{
    public class FotoService
    {
        private readonly HttpClient cliente;

        public FotoService(HttpClient cliente)
        {
            this.cliente = cliente;
        }

        public async void TomarFoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    using Stream sourceStream = await photo.OpenReadAsync();

                    byte[] buffer = new byte[sourceStream.Length];
                    sourceStream.ReadExactly(buffer, 0, buffer.Length);

                    var base64 = Convert.ToBase64String(buffer);

                    await SubirFoto(base64);
                }
            }
        }

        public async Task SubirFoto(string imgBase64)
        {
            SubirFotoRequest req = new SubirFotoRequest { 
                ImagenBase64 = imgBase64,
            };
            var response = await cliente.PostAsJsonAsync("/fotos", req);
        }
    }
}
