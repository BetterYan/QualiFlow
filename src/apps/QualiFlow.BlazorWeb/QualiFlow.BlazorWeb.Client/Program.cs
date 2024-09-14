using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using QualiFlow.Identity.Component.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddQualiFlowIdentityComponent();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
