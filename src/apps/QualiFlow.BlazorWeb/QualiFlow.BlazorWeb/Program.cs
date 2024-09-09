using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using QualiFlow.BlazorWeb.Components;
using QualiFlow.EntityFrameworkCore.SqlServer;
using QualiFlow.Identity.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddQualiFlowIdentityApiServices();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"], b => b.MigrationsAssembly("QualiFlow.BlazorWeb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(QualiFlow.BlazorWeb.Client._Imports).Assembly,
        typeof(QualiFlow.Identity.Component._Imports).Assembly
    );

app.Run();
