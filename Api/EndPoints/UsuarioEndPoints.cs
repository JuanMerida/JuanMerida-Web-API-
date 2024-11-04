using Api.Models;  
using Microsoft.AspNetCore.Mvc;

namespace Api.EndPoints;

public static class UsuarioEndPoints{
    public static RouteGroupBuilder MapUsuarioEndPoints(this RouteGroupBuilder app){
        List<Usuario> usuarios = [
    new Usuario {Idusuario = 1, Nombre = "Lucas" , Email = "lucas12@gmail.com", Usuario1 = "lucardio", Contraseña ="1234"},
    new Usuario {Idusuario = 2, Nombre = "Nahuel" , Email = "nahuel142@gmail.com", Usuario1 = "Nahuel", Contraseña ="1223"},
    new Usuario {Idusuario = 3, Nombre = "Joel",  Email = "joel14@gmail.com", Usuario1 = "Joel", Contraseña ="12"}];

    //Usuario
app.MapPost("/usuario", ([FromBody] Usuario usuario) =>
{

    if (string.IsNullOrWhiteSpace(usuario.Nombre))
    {
        return Results.BadRequest("El nombre del usuario no puede ser vacío o nulo.");
    }
    usuarios.Add(usuario);
    return Results.Created($"/usuario/{usuario.Idusuario}", usuario);
});


app.MapGet("/usuarios", () =>
{
    return Results.Ok(usuarios);
});


app.MapGet("/usuario/{id}", (int id) =>
{
    var usuario = usuarios.FirstOrDefault(usuario => usuario.Idusuario == id);

    if (usuario == null)
    {
        return Results.NotFound($"Usuario con ID {id} no encontrado.");
    }
    return Results.Ok(usuario);
});


app.MapPut("/usuario/{id}", (int id, [FromBody] Usuario usuario) =>
{
    var usuarioAActualizar = usuarios.FirstOrDefault(usuario => usuario.Idusuario == id);
    if (usuarioAActualizar != null)
    {
        usuarioAActualizar.Email = usuario.Email;
        usuarioAActualizar.Usuario1=usuarioAActualizar.Usuario1;
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
});

app.MapDelete("/usuario/{id}", ([FromQuery] int Id) =>{
    var usuarioAEliminar = usuarios.FirstOrDefault(usuario => usuario.Idusuario == Id);
    if (usuarioAEliminar != null)
    {
        usuarios.Remove(usuarioAEliminar);
        return Results.NoContent(); 
    }
    else
    {
        return Results.NotFound(); 
    }
});
    
    return app;
    }
    
}