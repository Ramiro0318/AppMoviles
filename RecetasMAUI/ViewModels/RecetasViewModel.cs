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

        private string? texto;
        public string? TextoBusqueda { 
            get { return texto; } 
            set { texto = value;
                Filtrar();
            } 
        }
        public string? NombreCategoria { set; get; }


        private int idCategoriaSeleccionada = 0;
        public ObservableCollection<RecetaMenuDTO> Menu { get; set; } = [];
        List<RecetaMenuDTO> ListaMenus = [];

        public RecetaDTO? RecetaActiva ;
        private List<RecetaDTO> TodasRecetas = new();
        public bool isLoading { get; set; }

        public ICommand CategoriaCommand { get; set; }
        public ICommand SeleccionarRecetaCommand { get; set; }
        public RecetasViewModel()
        {
            GetCategorias();
            CategoriaCommand = new Command<int>(GetRecetaByCategorias);
            SeleccionarRecetaCommand = new Command<int>(GetReceta);
            
        }

        void Filtrar() 
        {
            ListaMenus.Clear();

            var menus = ListaMenus.Where(x => x.IdCategoria == 
            idCategoriaSeleccionada && ( TextoBusqueda != null && 
            x.Nombre.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase))).ToList();
            //foreach (var m in menus)
            //{
            //    Menu.Add(m);
            //}

            menus.ForEach(Menu.Add);
        }

        //KISS
        private async void GetRecetaByCategorias(int id)
        {
            isLoading = true;
            PropertyChanged?.Invoke(this, new (nameof(isLoading)));

            idCategoriaSeleccionada = id;
            NombreCategoria = Categorias.FirstOrDefault(x => x.Id == id)?.Nombre;
            PropertyChanged?.Invoke(this, new(nameof(NombreCategoria)));

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

            isLoading = false;
            PropertyChanged?.Invoke(this, new(nameof(isLoading)));

            await Shell.Current.GoToAsync("menurecetas");
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

        private async void GetReceta(int id) 
        {
            isLoading = true;
            PropertyChanged?.Invoke(this, new(nameof(isLoading)));
            var receta = TodasRecetas.FirstOrDefault(x => x.Id == id);
            if (receta == null)
            {
                receta = await service.GetReceta(id);
                if (receta == null) return;
                TodasRecetas.Add(receta);
            }

            RecetaActiva = receta;
            PropertyChanged?.Invoke(this, new(nameof(RecetaActiva)));

            await Shell.Current.GoToAsync("receta");

            isLoading = true;
            PropertyChanged?.Invoke(this, new(nameof(isLoading)));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
