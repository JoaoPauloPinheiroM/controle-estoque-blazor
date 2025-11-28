using ControleEstoque.WebAssembly;
using ControleEstoque.WebAssembly.Interfaces;
using ControleEstoque.WebAssembly.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

builder.Services.AddScoped<IProdutoEst, ProdEstServices>();
builder.Services.AddScoped<IEstoque, EstServices>();
builder.Services.AddScoped<IProdutoBase, ProdBaseServices>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
