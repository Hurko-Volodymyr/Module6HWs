using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response.Items;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<CatalogItemDto> GetCatalogItemById(int id);
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex);
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsByBrandAsync(string brand);
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsByTypeAsync(string type);
}