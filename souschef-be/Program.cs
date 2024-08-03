using Microsoft.AspNetCore.Http.HttpResults;
using souschef_be.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RecipeAppContext>();
builder.Services.AddScoped<IDbService, PgDbService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/msg", (Message msg, IDbService svc) =>
{
    Console.Out.WriteLine($"PG string: {Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__PG")}");
    var res = svc.SendMessage(msg);
    svc.Commit();
    return res;
});

app.MapGet("/msg/all", (IDbService svc) => svc.GetAllMessages());

app.MapGet("/msg/{id:int}", Results<Ok<Message>, NotFound> (int id, IDbService svc) =>
{
    var msg = svc.GetMessage(id);
    return msg is null
        ? TypedResults.NotFound()
        : TypedResults.Ok(msg);
});

app.MapDelete("/msg/{id:int}", Results<NoContent, NotFound> (int id, IDbService svc) =>
{
    if (svc.DeleteMessage(id))
    {
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
});



app.Run();


internal interface IDbService
{
    Message? GetMessage(int id);
    List<Message> GetAllMessages();
    Message SendMessage(Message msg);
    bool DeleteMessage(int id);
    void Commit();
}

internal class PgDbService : IDbService
{
    private readonly SouschefContext _db = new();

    public Message? GetMessage(int id)
    {
        return _db.Messages.Find(id);
    }

    public List<Message> GetAllMessages()
    {
        try
        {
            return _db.Messages.ToList();
        }
        catch (ArgumentNullException e)
        {
            return [];
        }
    }

    public Message SendMessage(Message msg)
    {
        var res = _db.Messages.Add(msg).Entity;
        return res;
    }

    public bool DeleteMessage(int id)
    {
       var msg = _db.Messages.Find(id);
       if (msg is null)
       {
           return false;
       }
       _db.Messages.Remove(msg);
       return true;
    }

    public void Commit()
    {
        _db.SaveChanges();
    }
}