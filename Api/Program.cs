using Api;
using Api.Models;
using Api.EndPoints;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

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


app.Run();