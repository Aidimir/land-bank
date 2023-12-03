using Microsoft.AspNetCore.Mvc;
using Logic.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LandAssetsController : ControllerBase
{
    private readonly ILogger<LandAssetsController> _logger;

    private readonly ILandService _service;

    public LandAssetsController(ILogger<LandAssetsController> logger, ILandService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> FetchAssets()
    {
        var result = await _service.FetchLandAssets();

        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<NoContentResult> Delete(int id)
    {
        await _service.DeleteAsset(id);

        return NoContent();
    }
}

