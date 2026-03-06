using System;
using System.Collections.Generic;

namespace AsistenciasAPI.Models.Entities;

public partial class Grupo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Alumno> Alumno { get; set; } = new List<Alumno>();
}
