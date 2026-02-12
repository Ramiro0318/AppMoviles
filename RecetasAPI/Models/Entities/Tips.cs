using System;
using System.Collections.Generic;

namespace RecetasAPI.Models.Entities;

public partial class Tips
{
    public int Id { get; set; }

    public string? RecetaId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual Recetas? Receta { get; set; }
}
