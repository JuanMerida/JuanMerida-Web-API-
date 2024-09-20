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

app.MapGet("/usuario", (int id) =>
{
    var usuario = usuarios.FirstOrDefault(u => u.IdUsuario == id);
    if (usuario == null)
    {
        return Results.NotFound($"Usuario con ID {id} no encontrado.");
    }
    return Results.Ok(usuario);
})
    .WithTags("Alumno");
app.Run();