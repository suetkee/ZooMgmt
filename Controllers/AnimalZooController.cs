using AnimalZoo.Model;
using Microsoft.AspNetCore.Mvc;
using AnimalZoo.Database;
using Microsoft.EntityFrameworkCore;


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

    [HttpGet()]
    public async Task<ActionResult<List<Animal>>> GetAllAnimals()
    {
        var animals = await _context.Animals.ToListAsync();
        if (animals == null)
        {
            return NotFound();
        }
        return animals;
    }

    [HttpGet("search")]
    public async Task<ActionResult<PagedResponse<Animal>>> GetItems([FromQuery] PaginationParams paginationParams)
    {
// public async Task<ActionResult<IEnumerable<Animal>>> SearchAnimals(
//     string? searchQuery, int pageSize = 10, int pageNumber = 1, string orderBy = "Species", int? age = null)
// {
        var query = _context.Animals.AsQueryable();

        if (!string.IsNullOrEmpty(paginationParams.SearchQuery))
        {
            query = query.Where(a =>
            a.Species.Contains(paginationParams.SearchQuery) ||
            a.Classification.Contains(paginationParams.SearchQuery) ||
            a.Name.Contains(paginationParams.SearchQuery) ||
            a.DateAcquired.ToString().Contains(paginationParams.SearchQuery));
        }
            
            if(paginationParams.Age.HasValue){
            DateOnly ageThreshold = DateOnly.FromDateTime(DateTime.Now.AddYears((int)-paginationParams.Age));
            query = query.Where(a => a.DateofBirth <= ageThreshold);
            }

    //             if (age.HasValue)
    // {
    //     DateOnly ageThreshold = DateOnly.FromDateTime(DateTime.Now.AddYears(-age.Value));
    //     query = query.Where(a => a.DateOfBirth <= ageThreshold);
    // }
       





        var totalRecords = await query.CountAsync();
        var items = await query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                                .Take(paginationParams.PageSize)
                                .ToListAsync();

        var pagedResponse = new PagedResponse<Animal>(items, paginationParams.PageNumber, paginationParams.PageSize, totalRecords);

        return Ok(pagedResponse);
    }
}



