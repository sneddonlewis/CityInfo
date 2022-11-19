using CityInfo.API.Models;

namespace CityInfo.API;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; }
    public static CitiesDataStore Current { get; } = new CitiesDataStore();

    private CitiesDataStore()
    {
        Cities = new List<CityDto>()
        {
            new CityDto()
            {
                Id = 1,
                Name = "New York City",
                Description = "The one with the big park",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1,
                        Name = "Central Park",
                        Description = "The most visited urban park"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "Empire State Building",
                        Description = "A 102 story skyscraper"
                    },
                }
            },
            new CityDto()
            {
                Id = 2,
                Name = "London",
                Description = "My city",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1,
                        Name = "Liverpool Street Station",
                        Description = "An entry point to the city"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "The Cheese Grater",
                        Description = "A skyscraper that looks like a cheese grater"
                    },
                }
            },
        };
    }
}