using System.Runtime.CompilerServices;
using souschef_core.Exceptions;
using souschef_core.Services;
using souschef_fe.Components;
using souschef_fe.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IMessageSvc, ClientMessageService>();
builder.Services.AddHttpClient("WebAPI",
    client => client.BaseAddress =
        new Uri(builder.Configuration["BackendUrl"] ?? throw new MissingUriException("Souschef-fe client base address")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();