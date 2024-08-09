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
builder.Services.AddScoped<IMessageSvc, MsgSvcComponent>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/msg", async (Message msg, IMessageSvc svc) => await svc.AddMessageAsync(msg));

app.MapGet("/msg/all", (IMessageSvc svc) => svc.GetAllMessagesAsync());

app.MapGet("/msg/{id:long}", async Task<Results<Ok<Message>, NotFound>> (int id, IMessageSvc svc) =>
{
    var msg = await svc.GetMessageAsync(id);
    return msg is null
        ? TypedResults.NotFound()
        : TypedResults.Ok(msg);
});

app.MapDelete("/msg/{id:long}", async Task<Results<NoContent, NotFound>> (int id, IMessageSvc svc) =>
{
    if ( await svc.DeleteMessageAsync(id))
    {
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
});



app.Run();




