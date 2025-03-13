namespace AnimalZoo.Database; 
using Microsoft.EntityFrameworkCore;
using AnimalZoo.Model;


public class AnimalDbContext : DbContext
{
    public DbSet<Animal> Animals { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ZooManagement.db");
    }

}