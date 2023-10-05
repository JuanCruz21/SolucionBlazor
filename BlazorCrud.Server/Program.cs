using BlazorCrud.Server.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CrudblazorContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSql"));
});

builder.Services.AddCors(opciones => opciones.AddPolicy("Nueva politica", app =>
{
    app.AllowAnyOrigin().AllowAnyOrigin().AllowAnyMethod();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Nueva politica");
app.UseAuthorization();

app.MapControllers();

app.Run();
