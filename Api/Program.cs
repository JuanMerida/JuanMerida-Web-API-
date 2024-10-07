using Api;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



List<Rol> roles = [
    new Rol{ IdRol=1, Nombre="Estudiante"},
    new Rol{ IdRol=2, Nombre="Profesor"}
    ];




app.MapPost("/usuario/{idUsuario}/rol/{idRol}", (int idUsuario, int idRol) =>{
    var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
    var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);

    if (rol != null &&  usuario!= null)
    {
        usuario.Roles.Add(rol);
        return Results.Ok();
    }

    return Results.NotFound();

})
.WithTags("usuario");


app.MapDelete("/usuario/{idUsuario}/rol/{idRol}",(int idUsuario, int idRol) =>{
    var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
    var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);

    if (rol != null &&  usuario!= null)
    {
        usuario.Roles.Remove(rol);
        return Results.Ok();
    }

    return Results.NotFound();
}).WithTags("usuario");


//Rol

app.MapPost("/rol", ([FromBody] Rol rol) =>
{

    if (string.IsNullOrWhiteSpace(rol.Nombre))
    {
        return Results.BadRequest("El nombre del usuario no puede ser vacío o nulo.");
    }
    roles.Add(rol);
    return Results.Created($"/usuario/{rol.IdRol}", rol);
})
    .WithTags("rol");

app.MapGet("/roles", () =>
{
    return Results.Ok(roles);
})
.WithTags("rol");

app.MapGet("/rol/{id}", (int id) =>
{
    var rol = roles.FirstOrDefault(rol => rol.IdRol == id);

    if (rol == null)
    {
        return Results.NotFound($"Usuario con ID {id} no encontrado.");
    }
    return Results.Ok(rol);
})
.WithTags("rol");


app.MapPut("/rol/{id}", (int id, [FromBody] Rol rolActualizado) =>
{
    var rolAActualizar = roles.FirstOrDefault(rol => rol.IdRol == id);
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
})
.WithTags("rol");


app.MapDelete("/rol/{id}", ([FromQuery] int Id) =>
{
    var rolAEliminar = roles.FirstOrDefault(rol => rol.IdRol == Id);
    if (rolAEliminar != null)
    {
        roles.Remove(rolAEliminar);
        return Results.NoContent(); //Codigo 200
    }
    else
    {
        return Results.NotFound(); //Codigo 404
    }
})
    .WithTags("rol");


    app.MapPost("/rol/{idRol}/usuario/{idUsuario}", (int idUsuario, int idRol) =>{
    var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
    var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);

    if (rol != null &&  usuario!= null)
    {
        rol.Usuarios.Add(usuario);
        return Results.Ok();
    }

    return Results.NotFound();

})
.WithTags("rol");


app.MapDelete("/rol/{idRol}/usuario/{idUsuario}",(int idUsuario, int idRol) =>{
    var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
    var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);

    if (rol != null &&  usuario!= null)
    {
        rol.Usuarios.Remove(usuario);
        return Results.Ok();
    }

    return Results.NotFound();
}).WithTags("rol");

app.Run();