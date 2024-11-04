using Api.Models;  
using Microsoft.AspNetCore.Mvc;

namespace Api.EndPoints;

public static class UsuarioEndPoints{
public static RouteGroupBuilder MapUsuarioEndPoints(this RouteGroupBuilder app){
    //     List<Usuario> usuarios = [
    // new Usuario {Idusuario = 1, Nombre = "Lucas" , Email = "lucas12@gmail.com", Usuario1 = "lucardio", Contraseña ="1234"},
    // new Usuario {Idusuario = 2, Nombre = "Nahuel" , Email = "nahuel142@gmail.com", Usuario1 = "Nahuel", Contraseña ="1223"},
    // new Usuario {Idusuario = 3, Nombre = "Joel",  Email = "joel14@gmail.com", Usuario1 = "Joel", Contraseña ="12"}];

    //Usuario
app.MapPost("/usuario", ([FromBody] Usuario usuario, EscuelaContext context) =>
{

    if (string.IsNullOrWhiteSpace(usuario.Nombre))
    {
        return Results.BadRequest("El nombre del usuario no puede ser vacío o nulo.");
    }
    context.Usuarios.Add(usuario);
    context.SaveChanges();

    return Results.Created($"/usuario/{usuario.Idusuario}", usuario);
});


app.MapGet("/usuarios", (EscuelaContext context) =>
{
    return Results.Ok(context.Usuarios.ToList());
});


app.MapGet("/usuario/{id}",(int id,EscuelaContext context ) =>
{
    var usuario = context.Usuarios.FirstOrDefault(usuario => usuario.Idusuario == id);

    if (usuario == null)
    {
        return Results.NotFound($"Usuario con ID {id} no encontrado.");
    }
    return Results.Ok(usuario);
});


app.MapPut("/usuario/{id}", (int id, [FromBody] Usuario usuario, EscuelaContext context) =>
{
    var usuarioAActualizar = context.Usuarios.FirstOrDefault(usuario => usuario.Idusuario == id);
    if (usuarioAActualizar != null)
    {
        usuarioAActualizar.Email = usuario.Email;
        usuarioAActualizar.Usuario1=usuarioAActualizar.Usuario1;
        usuarioAActualizar.Contraseña=usuario.Contraseña;
        return Results.Ok(context.Usuarios); 
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
    context.SaveChanges();
    return Results.NoContent();
});

app.MapDelete("/usuario/{id}", ([FromQuery] int Id, EscuelaContext context) =>{
    var usuarioAEliminar = context.Usuarios.FirstOrDefault(usuario => usuario.Idusuario == Id);
    if (usuarioAEliminar != null)
    {
        context.Usuarios.Remove(usuarioAEliminar);
        context.SaveChanges();
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