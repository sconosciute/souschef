using Microsoft.AspNetCore.Http.HttpResults;
using souschef_be.models;
using souschef_be.Services;
using souschef_core.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddDebug();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RecipeAppContext>();
builder.Services.AddScoped<IBeMessageSvc, PgDbService>();
builder.Services.AddScoped<IMeasurementSvc, PgDbService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/msg", async (Message msg, IBeMessageSvc svc) =>
{
    var res = await svc.SendMessageAsync(msg);
    await svc.CommitAsync();
    return res;
});

app.MapGet("/msg/all", (IBeMessageSvc svc) => svc.GetAllMessagesAsync());

app.MapGet("/msg/{id:long}", async Task<Results<Ok<Message>, NotFound>> (int id, IBeMessageSvc svc) =>
{
    var msg = await svc.GetMessageAsync(id);
    return msg is null
        ? TypedResults.NotFound()
        : TypedResults.Ok(msg);
});

app.MapDelete("/msg/{id:long}", async Task<Results<NoContent, NotFound>> (int id, IBeMessageSvc svc) =>
{
    if ( await svc.DeleteMessageAsync(id))
    {
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
});

app.MapGet("/measurement", (IMeasurementSvc svc) => svc.GetAllMeasures());



app.Run();




