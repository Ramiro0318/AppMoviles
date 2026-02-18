using RecetasAPI.Models.DTOs;
using RecetasMAUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RecetasMAUI.ViewModels
{
    public class RecetasViewModel : INotifyPropertyChanged
    {

        public RecetasService service = new();
        public ObservableCollection<CategoriaDTO> Categorias { get; set; } = new();

        public RecetasViewModel()
        {
            GetCategorias();
        }

        public async void GetCategorias()
        {
            var categorias = await service.GetCategoria();
            Categorias.Clear();
            //Categorias.ForEach(categorias.Add);
            foreach (var c in categorias)
            {
                Categorias.Add(c);
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
