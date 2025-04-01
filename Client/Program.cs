using Blazored.LocalStorage;
using Functions.Client;
using Functions.Client.Proxys;
using Functions.Client.Proxys.Auth;
using Functions.Client.Services;
using Functions.Shared.Interfaces;
using Functions.Shared.Interfaces.Auth;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiClient:BaseAddress"]) });

builder.Services.AddScoped<IEventsProxy, EventProxy>();
builder.Services.AddScoped<IAuthProxy, AuthProxy>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();
