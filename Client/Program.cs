using Functions.Client;
using Functions.Client.Proxys;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiClient:BaseAddress"]) });

builder.Services.AddScoped<IEventsProxy, EventProxy>();

await builder.Build().RunAsync();
