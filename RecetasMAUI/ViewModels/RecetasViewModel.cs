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
            public ObservableCollection<CategoriaDTO>? Categorias { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
