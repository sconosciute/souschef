using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace souschef_be.models;

public class RecipeAppContext : DbContext
{
    public DbSet<TestMessages> Messages { get; set; }

    public string? PgConn { get; } = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__PG");

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        Console.Out.WriteLine(PgConn);
        options.UseNpgsql(PgConn);
    }
}

public class TestMessages
{
    [Key]
    public int MsgId { get; init; }
    public string Message { get; set; } = null!;
}