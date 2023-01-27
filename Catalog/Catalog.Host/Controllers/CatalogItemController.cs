using Catalog.Host.Models.Requests.Items;
using Catalog.Host.Models.Response;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogItemController : ControllerBase
{
    private readonly ILogger<CatalogItemController> _logger;
    private readonly ICatalogItemService _catalogItemService;

    public CatalogItemController(
        ILogger<CatalogItemController> logger,
        ICatalogItemService catalogItemService)
    {
        _logger = logger;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateUpdateItemRequest request)
    {
        var result = await _catalogItemService.AddAsync(request.Name, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(UpdateResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(int id, CreateUpdateItemRequest request)
    {
        var result = await _catalogItemService.UpdateAsync(id, request.Name, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);
        return Ok(new UpdateResponse() { IsUpdated = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(DeleteResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _catalogItemService.DeleteAsync(id);
        return Ok(new DeleteResponse() { IsDeleted = result });
    }
}