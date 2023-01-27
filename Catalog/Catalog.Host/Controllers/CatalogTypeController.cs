using Catalog.Host.Models.Requests.Types;
using Catalog.Host.Models.Response;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(
                ICatalogTypeService catalogTypeService,
                ILogger<CatalogTypeController> logger)
    {
        _logger = logger;
        _catalogTypeService = catalogTypeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateUpdateTypeRequest request)
    {
        var result = await _catalogTypeService.AddAsync(request.Type);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(UpdateResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(int id, CreateUpdateTypeRequest request)
    {
        var result = await _catalogTypeService.UpdateAsync(id, request.Type);
        return Ok(new UpdateResponse() { IsUpdated = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(DeleteResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _catalogTypeService.DeleteAsync(id);
        return Ok(new DeleteResponse() { IsDeleted = result });
    }
}