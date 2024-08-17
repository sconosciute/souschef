using System.Runtime.CompilerServices;
using souschef_core.Exceptions;
using souschef_core.Model;
using souschef_core.Model.DTO;
using souschef_core.Services;
using souschef_fe.Components;
using souschef_fe.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ISearchSvc, ClientSearchService>();

builder.Services.AddScoped<ICrudSvc<Tag>, ClientTagService>();

builder.Services.AddScoped<ClientRecipeService, ClientRecipeService>();

builder.Services.AddScoped<IMetricSvc, ClientMetricsService>();

builder.Services.AddScoped<ICrudSvc<User>, ClientUserService>();

builder.Services.AddHttpClient("WebAPI",
    client => client.BaseAddress =
        new Uri(builder.Configuration["BackendUrl"] ?? throw new MissingUriException("Souschef-fe client base address")));

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

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