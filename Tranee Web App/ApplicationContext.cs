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
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "test.db" };
        var connectionString = connectionStringBuilder.ToString();
        var connection = new SqliteConnection(connectionString);
        optionsBuilder.UseSqlite(connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // ToDoTask task1 = new ToDoTask { Id = 1, TaskDescription = "Third task" };
        //
        // User user = new User {Id = 1, Name = "Sergey", Password = "000"};
        // User user1 = new User {Name = "Sergey", Password = "000", ToDoTask = task1};

        modelBuilder.Entity<User>().HasData(new User{Id = 1, Name = "Admin", Password = "123"});
        modelBuilder.Entity<ToDoTask>(o =>
            o.HasData(new ToDoTask()
            {
                Id = 1,
                TaskDescription = "Test",
                UserId = 1
            })
    );
        // modelBuilder.Entity<User>().HasData(user);
        
        // modelBuilder.Entity<ToDoTask>().HasData(new ToDoTask{Id = 2, TaskDescription = "Second test task", Selected = true});
    }
}