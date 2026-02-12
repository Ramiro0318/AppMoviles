using System;
using System.Collections.Generic;

namespace RecetasAPI.Models.Entities;

public partial class Pasospreparacion
{
    public int Id { get; set; }

    public string? RecetaId { get; set; }

    public int NumeroPaso { get; set; }

    public string Descripcion { get; set; } = null!;

    public string? Seccion { get; set; }

    public virtual Recetas? Receta { get; set; }
}
