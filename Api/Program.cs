using Api;
using Api.Models;
using Api.EndPoints;
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
    app.UseSwaggerUI(options => options.EnableTryItOutByDefault());
}


app.MapGroup("/api")
    .MapUsuarioEndPoints()
    .WithTags("usuario");

app.MapGroup("/api")
    .MapRolEndPoints()
    .WithTags("rol");

// app.MapPost("/usuario/{idUsuario}/rol/{idRol}", (int idUsuario, int idRol) =>{
//     var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
//     var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);

//     if (rol != null &&  usuario!= null)
//     {
//         usuario.Roles.Add(rol);
//         return Results.Ok();
//     }

//     return Results.NotFound();

// })
// .WithTags("usuario");

// app.MapDelete("/usuario/{idUsuario}/rol/{idRol}",(int idUsuario, int idRol) =>{
//     var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
//     var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);

//     if (rol != null &&  usuario!= null)
//     {
//         usuario.Roles.Remove(rol);
//         return Results.Ok();
//     }

//     return Results.NotFound();
// }).WithTags("usuario");

//     app.MapPost("/rol/{idRol}/usuario/{idUsuario}", (int idUsuario, int idRol) =>{
//     var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
//     var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);

//     if (rol != null &&  usuario!= null)
//     {
//         rol.Usuarios.Add(usuario);
//         return Results.Ok();
//     }

//     return Results.NotFound();

// })
// .WithTags("rol");

// app.MapDelete("/rol/{idRol}/usuario/{idUsuario}",(int idUsuario, int idRol) =>{
//     var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
//     var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);

//     if (rol != null &&  usuario!= null)
//     {
//         rol.Usuarios.Remove(usuario);
//         return Results.Ok();
//     }

//     return Results.NotFound();
// }).WithTags("rol");

app.Run();