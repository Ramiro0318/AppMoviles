using System;
using System.Collections.Generic;

namespace AsistenciasAPI.Models.Entities;

public partial class Alumno
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string NumControl { get; set; } = null!;

    public int? IdGrupo { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual Grupo? IdGrupoNavigation { get; set; }
}
