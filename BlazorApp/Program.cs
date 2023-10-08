using BlazorApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorCrud.Shared;
using BlazorApp.Service;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5015") });
builder.Services.AddScoped<IDepartamentoServices, DepartamentoService>();
builder.Services.AddScoped<IEmpleadosServer,EmpleadoService>();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
