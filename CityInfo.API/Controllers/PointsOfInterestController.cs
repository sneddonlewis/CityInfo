using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities/{cityId}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase
{
    private readonly ILogger<PointsOfInterestController> _logger;
    private readonly CitiesDataStore _citiesDataStore;

    public PointsOfInterestController(ILogger<PointsOfInterestController> logger, CitiesDataStore citiesDataStore)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
    }

    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
    {
        var city = _citiesDataStore
            .Cities
            .FirstOrDefault(c => c.Id == cityId);
        return city == null ? NotFound() : Ok(city);
    }

    [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
        var city = _citiesDataStore
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

    [HttpPost]
    public ActionResult<PointOfInterestDto> CreatePointOfInterest(
        int cityId,
        PointOfInterestForCreationDto pointOfInterest)
    {
        var city = _citiesDataStore
            .Cities
            .FirstOrDefault(c => c.Id == cityId);

        if (city == null)
        {
            return NotFound();
        }

        var maxPoiId = _citiesDataStore
            .Cities
            .SelectMany(c => c.PointsOfInterest)
            .Max(p => p.Id);

        var createdPoi = new PointOfInterestDto()
        {
            Id = ++maxPoiId,
            Name = pointOfInterest.Name,
            Description = pointOfInterest.Description
        };
        
        city.PointsOfInterest.Add(createdPoi);
        
        return CreatedAtRoute(
            "GetPointOfInterest",
            new
            {
                cityId = cityId,
                pointOfInterestId = createdPoi.Id
            },
            createdPoi);
    }

    [HttpPut("{pointofinterestid}")]
    public ActionResult UpdatePointOfInterest(
        int cityId,
        int pointOfInterestId,
        PointOfInterestForUpdateDto pointOfInterest)
    {
        var city = _citiesDataStore
            .Cities
            .FirstOrDefault(c => c.Id == cityId);

        if (city == null)
        {
            return NotFound();
        }

        var pointOfInterestFromStore = city
            .PointsOfInterest
            .FirstOrDefault(p => p.Id == pointOfInterestId);

        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }

        pointOfInterestFromStore.Name = pointOfInterest.Name;
        pointOfInterestFromStore.Description = pointOfInterest.Description;

        return NoContent();
    }

    [HttpPatch("{pointofinterestid}")]
    public ActionResult PartiallyUpdatePointOfInterest(
        int cityId,
        int pointOfInterestId,
        JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
    {
        var city = _citiesDataStore
            .Cities
            .FirstOrDefault(c => c.Id == cityId);

        if (city == null)
        {
            return NotFound();
        }

        var pointOfInterestFromStore = city
            .PointsOfInterest
            .FirstOrDefault(p => p.Id == pointOfInterestId);

        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }

        var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
        {
            Name = pointOfInterestFromStore.Name,
            Description = pointOfInterestFromStore.Description
        };
        
        patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!TryValidateModel(pointOfInterestToPatch))
        {
            return BadRequest(ModelState);
        }

        pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
        pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

        return NoContent();
    }

    [HttpDelete("{pointofinterestid}")]
    public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
    {
        var city = _citiesDataStore
            .Cities
            .FirstOrDefault(c => c.Id == cityId);

        if (city == null)
        {
            return NotFound();
        }

        var pointOfInterestFromStore = city
            .PointsOfInterest
            .FirstOrDefault(p => p.Id == pointOfInterestId);

        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }

        city.PointsOfInterest.Remove(pointOfInterestFromStore);

        return NoContent();
    }
}