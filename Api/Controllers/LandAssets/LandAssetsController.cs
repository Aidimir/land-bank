using Microsoft.AspNetCore.Mvc;
using Logic.Interfaces;
using Dal.Models;
using Api.Controllers.LandAssets.DTO.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Dal.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class LandAssetsController : ControllerBase
{
    private readonly ILandService _service;

    public LandAssetsController(ILogger<LandAssetsController> logger, ILandService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> FetchAssets(int? id = null,
            string? owner = null,
            string? fullname = null,
            LandType? type = null)
    {
        var result = await _service.FetchLandAssets(id, owner, fullname, type);
        return Ok(result);
    }


    [HttpPost]
    [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<ActionResult> CreateAsset(LandAssetRequestModel asset)
    {
        var landAsset = asset.CreateLandAsset();
        var result = await _service.CreateAsset(landAsset);

        return StatusCode(201, result);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsset(int id, LandAssetRequestModel asset)
    {
        var landAsset = asset.CreateLandAsset();
        var updatedAsset = await _service.UpdateAsset(id, landAsset);

        return StatusCode(200, updatedAsset);
    }

    [HttpDelete]
    [Authorize(Roles = RoleConstants.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<NoContentResult> Delete(int id)
    {
        await _service.DeleteAsset(id);

        return NoContent();
    }
}

