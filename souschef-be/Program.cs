using Microsoft.AspNetCore.Http.HttpResults;
using FastEndpoints;
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
builder.Services.AddFastEndpoints();

builder.Services.AddDbContext<DbContext, SouschefContext>();
builder.Services.AddScoped<ICrudSvc<Message>, PgCrudSvcComponent<Message>>();
builder.Services.AddScoped<ICrudSvc<User>, PgCrudSvcComponent<User>>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseFastEndpoints();


app.Run();




