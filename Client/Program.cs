using Blazored.LocalStorage;
using Functions.Client;
using Functions.Client.Proxys.Auth;
using Functions.Client.Proxys.Events;
using Functions.Client.Proxys.Message;
using Functions.Client.Proxys.Participation;
using Functions.Client.Proxys.Search;
using Functions.Client.Proxys.Users;
using Functions.Client.Services;
using Functions.Shared.Interfaces;
using Functions.Shared.Interfaces.Auth;
using Functions.Shared.Interfaces.Messages;
using Functions.Shared.Interfaces.Participation;
using Functions.Shared.Interfaces.Search;
using Functions.Shared.Interfaces.User;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiClient:BaseAddress"]) });

builder.Services.AddScoped<IEventsProxy, EventProxy>();
builder.Services.AddScoped<IAuthProxy, AuthProxy>();
builder.Services.AddScoped<IMessageProxy, MessageProxy>();
builder.Services.AddScoped<IParticipationProxy, ParticipationProxy>();
builder.Services.AddScoped<IUserProxy, UserProxy>();
builder.Services.AddScoped<ISearchProxy, SearchProxy>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();
