using System;
using System.Collections.Generic;

namespace PendientesAPI.Models.Entities;

public partial class Usuarios
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string ContraseñaHash { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Pendientes> Pendientes { get; set; } = new List<Pendientes>();
}
