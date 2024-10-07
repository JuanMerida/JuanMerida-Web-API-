using Api.Models;  
using Microsoft.AspNetCore.Mvc;

namespace Api.EndPoints;

public static class UsuarioEndPoints{
    public static RouteGroupBuilder MapUsuarioEndPoints(this RouteGroupBuilder app){
        List<Usuario> usuarios = [
    new Usuario {IdUsuario = 1, Nombre = "Lucas" , Email = "lucas12@gmail.com", usuario = "lucardio", Contraseña ="1234"},
    new Usuario {IdUsuario = 2, Nombre = "Nahuel" , Email = "nahuel142@gmail.com", usuario = "Nahuel", Contraseña ="1223"},
    new Usuario {IdUsuario = 3, Nombre = "Joel",  Email = "joel14@gmail.com", usuario = "Joel", Contraseña ="12"}];

    //Usuario
app.MapPost("/usuario", ([FromBody] Usuario usuario) =>
{

    if (string.IsNullOrWhiteSpace(usuario.Nombre))
    {
        return Results.BadRequest("El nombre del usuario no puede ser vacío o nulo.");
    }
    usuarios.Add(usuario);
    return Results.Created($"/usuario/{usuario.IdUsuario}", usuario);
})
    .WithTags("usuario");


app.MapGet("/usuarios", () =>
{
    return Results.Ok(usuarios);
})
.WithTags("usuario");


app.MapGet("/usuario/{id}", (int id) =>
{
    var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == id);

    if (usuario == null)
    {
        return Results.NotFound($"Usuario con ID {id} no encontrado.");
    }
    return Results.Ok(usuario);
}).WithTags("usuario");


app.MapPut("/usuario/{id}", (int id, [FromBody] Usuario usuario) =>
{
    var usuarioAActualizar = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == id);
    if (usuarioAActualizar != null)
    {
        usuarioAActualizar.Email = usuario.Email;
        usuarioAActualizar.usuario=usuarioAActualizar.usuario;
        usuarioAActualizar.Contraseña=usuario.Contraseña;
        return Results.Ok(usuarios); 
    }
    
    if (usuarioAActualizar == null)
    {
        return Results.NotFound($"Usuario con ID {id} no encontrado.");
    }
    if (!string.IsNullOrWhiteSpace(usuario.Nombre))
    {
        return Results.BadRequest("No se puede modificar el nombre del usuario.");
    }
    usuarioAActualizar.Habilitado = usuario.Habilitado;
    return Results.NoContent();
})
.WithTags("usuario");

app.MapDelete("/usuario/{id}", ([FromQuery] int Id) =>{
    var usuarioAEliminar = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == Id);
    if (usuarioAEliminar != null)
    {
        usuarios.Remove(usuarioAEliminar);
        return Results.NoContent(); 
    }
    else
    {
        return Results.NotFound(); 
    }
})
    .WithTags("usuario");
    
    return app;
    }
    
}