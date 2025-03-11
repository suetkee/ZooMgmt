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


    // private readonly ILogger<AnimalZooController> _logger;

    // public AnimalZooController(ILogger<AnimalZooController> logger)
    // {
    //     _logger = logger;
    // }

    // [HttpGet(Name = "GetAnimalById")]
     [HttpGet("{id}")]
    public async Task<IActionResult> Get(int? id)
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

        return Ok(animal);





        // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        // {
        //     Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //     TemperatureC = Random.Shared.Next(-20, 55),
        //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        // })
        // .ToArray();
    }
}
