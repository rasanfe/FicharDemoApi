using FicharApi.Services.Impl;
using DWNet.Data.AspNetCore;
using SnapObjects.Data;
using SnapObjects.Data.AspNetCore;
using SnapObjects.Data.SqlServer;
using System.IO.Compression;
using FicharApi;
using FicharApi.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(m =>
{
    m.UseCoreIntegrated();
    m.UsePowerBuilderIntegrated();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger
builder.Services.AddSwaggerGen();


builder.Services.AddGzipCompression(CompressionLevel.Fastest);
builder.Services.AddHttpContextAccessor();


builder.Services.AddDataContext<DefaultDataContext>(m => m.UseSqlServer(builder.Configuration.GetConnectionString("FicharDemo")));



builder.Services.AddScoped<INomregistroService, NomregistroService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IEmpleadosService, EmpleadosService>();
builder.Services.AddScoped<ISistemaService, SistemaService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
 }


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.UseResponseCompression();

app.UseDataWindow();

app.Run();
