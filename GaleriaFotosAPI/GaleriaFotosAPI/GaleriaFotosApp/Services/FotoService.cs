using GaleriaFotosApp.DTOs;
using Java.Util;
using Org.W3c.Dom.LS;
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

        public async BackgroundTasks<IEnumerable<FotoDto>> GetAllAsync() 
        {
            var fotos => await 
        }

        public async Task<string?> TomarFoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                    using Stream sourceStream = await photo.OpenReadAsync();

                    using FileStream localFileStream = File.OpenWrite(localFilePath);
                    await sourceStream.CopyToAsync(localFileStream);

                    byte[] buffer = new byte[sourceStream.Length];
                    sourceStream.ReadExactly(buffer, 0, buffer.Length);

                    var base64 = Convert.ToBase64String(buffer);
                    await SubirFoto(base64);

                    return localFilePath;
                }
            }
            return null;
        }


        public async Task<string?> PickFoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult? photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                    using Stream sourceStream = await photo.OpenReadAsync();

                    using FileStream localFileStream = File.OpenWrite(localFilePath);
                    await sourceStream.CopyToAsync(localFileStream);

                    byte[] buffer = new byte[sourceStream.Length];
                    sourceStream.ReadExactly(buffer, 0, buffer.Length);

                    var base64 = Convert.ToBase64String(buffer);
                    await SubirFoto(base64);

                    return localFilePath;
                }
            }
            return null;
        }



        public async Task<int?> SubirFoto(string filepath)
        {
            var imgBase64 = Convert.ToBase64String(File.ReadAllBytes(filepath));

            SubirFotoRequest req = new SubirFotoRequest
            {
                ImagenBase64 = imgBase64,
            };

            var response = await cliente.PostAsJsonAsync("/fotos", req);

            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadFromJsonAsync<SubirFotoResponse>();

                if (resp != null)
                {
                    return resp.Id;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<FotoDto>> GetFotos() 
        {
            var response = await cliente.GetFromJsonAsync<List<FotoDto>("/fotos") ?? [];

            foreach (var archivo in response)
            {
                archivo.NombreArchivo = $"https://localhost:44303/uploads/{archivo.id}.jpg";
            }
            return response;
        }
    }
}
