using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using QualiFlow.Web.Client.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddQualiFlowServices();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
