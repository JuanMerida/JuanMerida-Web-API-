using Api.Models;  
using Microsoft.AspNetCore.Mvc;

namespace Api.EndPoints;

public static class RolEndPoints
{
    public static RouteGroupBuilder MapRolEndPoints(this RouteGroupBuilder app){
    List<Rol> roles = [
    new Rol{ Idrol=1, Nombre="Estudiante"},
    new Rol{ Idrol=2, Nombre="Profesor"}
    ];
    
    //Rol

app.MapPost("/rol", ([FromBody] Rol rol) =>
{

    if (string.IsNullOrWhiteSpace(rol.Nombre))
    {
        return Results.BadRequest("El nombre del usuario no puede ser vacío o nulo.");
    }
    roles.Add(rol);
    return Results.Created($"/usuario/{rol.Idrol}", rol);
})
    .WithTags("rol");

app.MapGet("/roles", () =>
{
    return Results.Ok(roles);
})
.WithTags("rol");

app.MapGet("/rol/{id}", (int id) =>
{
    var rol = roles.FirstOrDefault(rol => rol.Idrol == id);

    if (rol == null)
    {
        return Results.NotFound($"Usuario con ID {id} no encontrado.");
    }
    return Results.Ok(rol);
});


app.MapPut("/rol/{id}", (int id, [FromBody] Rol rolActualizado) =>
{
    var rolAActualizar = roles.FirstOrDefault(rol => rol.Idrol == id);
    if (rolAActualizar == null)
    {
        return Results.NotFound($"Usuario con ID {id} no encontrado.");
    }
    if (!string.IsNullOrWhiteSpace(rolAActualizar.Nombre))
    {
        return Results.BadRequest("No se puede modificar el nombre del usuario.");
    }
    rolAActualizar.Habilitado = rolAActualizar.Habilitado;
    return Results.NoContent();
});


app.MapDelete("/rol/{id}", ([FromQuery] int Id) =>
{
    var rolAEliminar = roles.FirstOrDefault(rol => rol.Idrol == Id);
    if (rolAEliminar != null)
    {
        roles.Remove(rolAEliminar);
        return Results.NoContent(); //Codigo 200
    }
    else
    {
        return Results.NotFound(); //Codigo 404
    }
});
        
        return app;
    }
    
}