using System;
using System.Collections.Generic;

namespace PendientesAPI.Models.Entities;

public partial class Pendientes
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Estado { get; set; } = null!;

    public int? IdUsuario { get; set; }

    public virtual Usuarios? IdUsuarioNavigation { get; set; }
}
