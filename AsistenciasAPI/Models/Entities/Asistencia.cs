using System;
using System.Collections.Generic;

namespace AsistenciasAPI.Models.Entities;

public partial class Asistencia
{
    public int Id { get; set; }

    public int IdAlumno { get; set; }

    public DateTime Fecha { get; set; }

    public int IdEstado { get; set; }

    public string? Motivo { get; set; }

    public virtual Alumno IdAlumnoNavigation { get; set; } = null!;

    public virtual Estado IdEstadoNavigation { get; set; } = null!;
}
