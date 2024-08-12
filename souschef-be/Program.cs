using Microsoft.AspNetCore.Http.HttpResults;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using souschef_be.models;
using souschef_be.Services;
using souschef_core.Model;
using souschef_core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddDebug();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = System.Environment.GetEnvironmentVariable("JWT_KEY"))
    .AddAuthorization()
    .AddFastEndpoints();

builder.Services.AddDbContext<DbContext, SouschefContext>();
builder.Services.AddScoped<ICrudSvc<Message>, PgCrudSvcComponent<Message>>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints();

app.Run();