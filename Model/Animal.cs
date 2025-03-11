using Microsoft.AspNetCore.Mvc;

namespace AnimalZoo.Model;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Sex { get; set; }
    public DateOnly DateofBirth { get; set; }
    public DateOnly DateAcquired { get; set; }
    public string Species { get; set; }
    public string Classification { get; set; }

    public Animal(int id, string name, string sex, DateOnly dateofbirth, DateOnly dateacquired, string species, string classification) {
        Id = id;
        Name = name;
        Sex = sex;
        DateofBirth = dateofbirth;
        DateAcquired = dateacquired;
        Species = species;
        Classification = classification;
    }
     public Animal() {}

    public static explicit operator Animal(NotFoundResult v)
    {
        throw new NotImplementedException();
    }
}