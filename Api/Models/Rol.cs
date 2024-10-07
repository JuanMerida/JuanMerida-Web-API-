using System;
using System.Collections.Generic;

namespace Api.Models;
public class Rol{
    public int IdRol{get; set;}
    public required string Nombre {get; set;}
    public bool Habilitado {get; set;} = true;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
public List<Usuario> Usuarios {get; set;} = new List<Usuario>();

}