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

List<Usuario> usuarios = [
    new Usuario {IdUsuario = 1, Nombre = "Lucas" , Email = "lucas12@gmail.com", usuario = "lucardio", Contraseña ="1234"},
    new Usuario {IdUsuario = 2, Nombre = "Nahuel" , Email = "nahuel142@gmail.com", usuario = "Nahuel", Contraseña ="1223"},
    new Usuario {IdUsuario = 3, Nombre = "Joel",  Email = "joel14@gmail.com", usuario = "Joel", Contraseña ="12"}
];

List<Rol> roles = [
    new Rol{ IdRol=1, Nombre="Estudiante"},
    new Rol{ IdRol=2, Nombre="Profesor"}
    ];

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
})
.WithTags("usuario");


app.MapPut("/usuario/{id}", (int id, [FromBody] Usuario usuarioActualizado) =>
{
    var usuarioAActualizar = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == id);

    if (usuarioAActualizar == null)
    {
        return Results.NotFound($"Usuario con ID {id} no encontrado.");
    }
    if (!string.IsNullOrWhiteSpace(usuarioActualizado.Nombre))
    {
        return Results.BadRequest("No se puede modificar el nombre del usuario.");
    }
    usuarioAActualizar.Habilitado = usuarioActualizado.Habilitado;
    return Results.NoContent();
})
.WithTags("usuario");


app.MapDelete("/usuario/{id}", ([FromQuery] int Id) =>
{
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





app.Run();