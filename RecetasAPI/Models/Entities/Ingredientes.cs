using System;
using System.Collections.Generic;

namespace RecetasAPI.Models.Entities;

public partial class Ingredientes
{
    public int Id { get; set; }

    public string? RecetaId { get; set; }

    public string IngredienteTexto { get; set; } = null!;

    public string? Seccion { get; set; }

    public int? Orden { get; set; }

    public virtual Recetas? Receta { get; set; }
}
