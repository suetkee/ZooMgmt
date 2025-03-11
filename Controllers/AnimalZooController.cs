using AnimalZoo.Model;
using Microsoft.AspNetCore.Mvc;
using AnimalZoo.Database;

namespace AnimalZoo.Controllers;

[ApiController]
[Route("[controller]")] 
public class AnimalZooController : ControllerBase
{
    private readonly AnimalDbContext _context;

    public AnimalZooController(AnimalDbContext context)
    {
        _context = context;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var animal = await _context.Animals.FindAsync(id);
        if (animal == null)
        {
            return NotFound();
        }
        return animal;
    }

   [HttpPost]
    public async Task<ActionResult<Animal>> PostNewAnimal(Animal animal)
    {
        _context.Animals.Add(animal);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
    }
}


