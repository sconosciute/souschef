using Microsoft.AspNetCore.Http.HttpResults;
using souschef_be.models;
using souschef_be.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RecipeAppContext>();
builder.Services.AddScoped<IMessageSvc, PgDbService>();
builder.Services.AddScoped<IMeasurementSvc, PgDbService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/msg", (Message msg, IMessageSvc svc) =>
{
    Console.Out.WriteLine($"PG string: {Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__PG")}");
    var res = svc.SendMessage(msg);
    svc.Commit();
    return res;
});

app.MapGet("/msg/all", (IMessageSvc svc) => svc.GetAllMessages());

app.MapGet("/msg/{id:int}", Results<Ok<Message>, NotFound> (int id, IMessageSvc svc) =>
{
    var msg = svc.GetMessage(id);
    return msg is null
        ? TypedResults.NotFound()
        : TypedResults.Ok(msg);
});

app.MapDelete("/msg/{id:int}", Results<NoContent, NotFound> (int id, IMessageSvc svc) =>
{
    if (svc.DeleteMessage(id))
    {
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
});

app.MapGet("/measurement", (IMeasurementSvc svc) => svc.GetAllMeasures());



app.Run();




