namespace Api;

public class Usuario{
    public int IdUsuario {get; set;}
    public required string Nombre {get; set;}
    public required string Email {get; set;}
    public required string usuario {get; set;}
    public required string Contraseña {get; set;}
    public bool Habilitado { get; set; } = true;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

}