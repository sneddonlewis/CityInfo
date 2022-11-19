using CityInfo.API.Models;

namespace CityInfo.API;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }

    public CitiesDataStore()
    {
        Cities = new List<CityDto>()
        {
            new CityDto()
            {
                Id = 1,
                Name = "New York City",
                Description = "The one with the big park."
            },
            new CityDto()
            {
                Id = 2,
                Name = "London",
                Description = "My city."
            },
        };
    }
}