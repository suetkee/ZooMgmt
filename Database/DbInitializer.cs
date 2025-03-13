
using Microsoft.EntityFrameworkCore;
using AnimalZoo.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace AnimalZoo.Database
{
    public static class DbInitializer
    {
        public static async Task SeedAnimals(AnimalDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (await context.Animals.AnyAsync())
            {
                return;
            }

            await SeedAnimalsFromJson(context, "Database/AnimalZooData.json");
        }

        private static async Task SeedAnimalsFromJson(AnimalDbContext context, string filePath)
        {
            if (!File.Exists(filePath)) return;

            string json = await File.ReadAllTextAsync(filePath);
            var animals = JsonSerializer.Deserialize<List<Animal>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            if (animals != null)
            {
                await context.Animals.AddRangeAsync(animals);
                await context.SaveChangesAsync();
            }
        }
    }
}