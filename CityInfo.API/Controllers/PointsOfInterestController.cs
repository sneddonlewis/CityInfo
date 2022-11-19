using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities/{cityId}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase 
{
    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
    {
        var city = CitiesDataStore
            .Current
            .Cities
            .FirstOrDefault(c => c.Id == cityId);
        return city == null ? NotFound() : Ok(city);
    }

    [HttpGet("{pointofinterestid}")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
        var city = CitiesDataStore
            .Current
            .Cities
            .FirstOrDefault(c => c.Id == cityId);
        
        if (city == null)
        {
            return NotFound();
        }

        var pointOfInterest = city
            .PointsOfInterest
            .FirstOrDefault(p => p.Id == pointOfInterestId);

        return pointOfInterest == null ? NotFound() : Ok(pointOfInterest);
    }
}