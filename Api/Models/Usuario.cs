using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Usuario1 { get; set; } = null!;

    public bool? Habilitado { get; set; }

    public DateTime? Fechacreacion { get; set; }
}
