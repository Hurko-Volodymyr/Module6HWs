using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response.Items;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<PaginatedItemsResponse<CatalogTypeDto>> GetCatalogTypes();

        Task<int?> AddAsync(string type);

        Task<bool> UpdateAsync(int id, string type);

        Task<bool> DeleteAsync(int id);
    }
}