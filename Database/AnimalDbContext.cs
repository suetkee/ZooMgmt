namespace AnimalZoo.Database; 
using Microsoft.EntityFrameworkCore;
using AnimalZoo.Model;


public class AnimalDbContext : DbContext
{
    // Put all the tables you want in your database here
    public DbSet<Animal> Animals { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ZooManagement.db");
    }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// {
//     string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyAnimalZooData", "ZooManagement.db");
//     optionsBuilder.UseSqlite($"Data Source={databasePath}");
// }


}


