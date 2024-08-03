using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace souschef_be.models;

public class RecipeAppContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    public string? PgConn { get; } = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__PG");

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(PgConn);
    }
}
