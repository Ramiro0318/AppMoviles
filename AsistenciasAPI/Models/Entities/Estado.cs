using System;
using System.Collections.Generic;

namespace AsistenciasAPI.Models.Entities;

public partial class Estado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();
}
