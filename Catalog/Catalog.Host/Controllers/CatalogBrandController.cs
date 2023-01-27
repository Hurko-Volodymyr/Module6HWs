using Catalog.Host.Models.Requests.Brands;
using Catalog.Host.Models.Response;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandController(
                ICatalogBrandService catalogBrandService,
                ILogger<CatalogBrandController> logger)
    {
        _catalogBrandService = catalogBrandService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateUpdateBrandRequest request)
    {
        var result = await _catalogBrandService.AddAsync(request.Brand);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(UpdateResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(int id, CreateUpdateBrandRequest request)
    {
        var result = await _catalogBrandService.UpdateAsync(id, request.Brand);
        return Ok(new UpdateResponse() { IsUpdated = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(DeleteResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _catalogBrandService.DeleteAsync(id);
        return Ok(new DeleteResponse() { IsDeleted = result });
    }
}