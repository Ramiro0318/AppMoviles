using System;
using System.Collections.Generic;

namespace RecetasAPI.Models.Entities;

public partial class Recetas
{
    public string Id { get; set; } = null!;

    public int? DatabaseId { get; set; }

    public string Titulo { get; set; } = null!;

    public string UrlAmigable { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Porcion { get; set; }

    public string? Tiempo { get; set; }

    public string? ImagenUrl { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? Beneficios { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Ingredientes> Ingredientes { get; set; } = new List<Ingredientes>();

    public virtual ICollection<Pasospreparacion> Pasospreparacion { get; set; } = new List<Pasospreparacion>();

    public virtual ICollection<Tips> Tips { get; set; } = new List<Tips>();

    public virtual ICollection<Categorias> Categoria { get; set; } = new List<Categorias>();
}
