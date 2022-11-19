using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
    [HttpGet()]
    public JsonResult GetCities()
    {
        return new JsonResult(CitiesDataStore.Current.Cities);
    }

    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCity(int id)
    {
        var cityToReturn = CitiesDataStore
            .Current
            .Cities
            .FirstOrDefault(c => c.Id == id);
        return cityToReturn == null ? NotFound() : Ok(cityToReturn);
    }
}