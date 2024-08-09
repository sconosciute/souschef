using Microsoft.AspNetCore.Http.HttpResults;
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
builder.Services.AddDbContext<SouschefContext>();
builder.Services.AddScoped<ICrudSvc<Message>, PgCrudSvcComponent<Message>>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/msg", async (Message msg, ICrudSvc<Message> svc) => await svc.AddAsync(msg));

app.MapGet("/msg/all", (ICrudSvc<Message> svc) => svc.GetAllAsync());

app.MapGet("/msg/{id:long}", async Task<Results<Ok<Message>, NotFound>> (int id, ICrudSvc<Message> svc) =>
{
    var msg = await svc.GetAsync(id);
    return msg is null
        ? TypedResults.NotFound()
        : TypedResults.Ok(msg);
});

app.MapDelete("/msg/{id:long}", async Task<Results<NoContent, NotFound>> (int id, ICrudSvc<Message> svc) =>
{
    if ( await svc.DeleteAsync(id))
    {
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
});



app.Run();




