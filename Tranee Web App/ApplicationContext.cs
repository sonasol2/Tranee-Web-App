using Microsoft.EntityFrameworkCore;
using Tranee_Web_App.Models;

namespace Tranee_Web_App;

public class ApplicationContext : DbContext
{
    public DbSet<ToDoTask> ToDoTasks { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    
    public ApplicationContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=test.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User{Id = 1, Name = "Admin", Password = "123"});
    }

}