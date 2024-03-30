using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public sealed class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ToDoTask> ToDoTasks { get; set; } = null!;
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "test.db" };
        var connectionString = connectionStringBuilder.ToString();
        var connection = new SqliteConnection(connectionString);
        optionsBuilder.UseSqlite(connection);
    }
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     
    // }
}