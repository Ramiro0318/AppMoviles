using System;
using System.Collections.Generic;

namespace RecetasAPI.Models.Entities;

public partial class Categorias
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string UrlAmigable { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Recetas> Receta { get; set; } = new List<Recetas>();
}
