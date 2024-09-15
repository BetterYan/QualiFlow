using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using QualiFlow.Identity.Component.Contracts;
using QualiFlow.Identity.Component.Extensions;
using QualiFlow.Identity.Component.HttpMessageHandler;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddQualiFlowIdentityComponent(builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();
