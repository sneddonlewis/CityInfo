using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    [HttpGet("{fileid}")]
    public ActionResult GetFile(string fileId)
    {
        var pathToFile = "demo-file.txt";
        if (!System.IO.File.Exists(pathToFile))
        {
            return NotFound();
        }

        var bytes = System.IO.File.ReadAllBytes(pathToFile);
        return File(bytes, "text/plain", Path.GetFileName(pathToFile));
    }
}