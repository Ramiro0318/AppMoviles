using RecetasAPI.Models.DTOs;
using RecetasMAUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Input;

namespace RecetasMAUI.ViewModels
{
    public class RecetasViewModel : INotifyPropertyChanged
    {

        public RecetasService service = new();
        public ObservableCollection<CategoriaDTO> Categorias { get; set; } = new();
        public ObservableCollection<RecetaMenuDTO> Menu { get; set; } = [];
        List<RecetaMenuDTO> ListaMenus = [];

        public ICommand CategoriaCommand { get; set; }
        public RecetasViewModel()
        {
            GetCategorias();
            CategoriaCommand = new Command<int>(GetRecetaByCategorias);
        }

        private async void GetRecetaByCategorias(int id)
        {
            var menus = ListaMenus.Where(x => x.IdCategoria == id).ToList();
            if (menus.Count() == 0)
            {
                //descargar de la api
                menus = await service.GetMenuPorCategoria(id);
                //Agregar a la lista
                ListaMenus.AddRange(menus);
            }
            Menu.Clear();
            menus.ForEach(Menu.Add);

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
