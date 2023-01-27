using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests.Items;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService,
        ICatalogBrandService brandService,
        ICatalogTypeService catalogTypeService)
    {
        _logger = logger;
        _catalogService = catalogService;
        _catalogBrandService = brandService;
        _catalogTypeService = catalogTypeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogBrandDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Brands()
    {
        var result = await _catalogBrandService.GetCatalogBrands();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Types()
    {
        var result = await _catalogTypeService.GetCatalogTypes();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsByBrand(ItemsByBrandRequest request)
    {
        var result = await _catalogService.GetCatalogItemsByBrandAsync(request.Brand);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsByType(ItemsByTypeRequest request)
    {
        var result = await _catalogService.GetCatalogItemsByTypeAsync(request.Type);
        return Ok(result);
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemById(int id)
    {
        var result = await _catalogService.GetCatalogItemById(id);
        return Ok(result);
    }
}