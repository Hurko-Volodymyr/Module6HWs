using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response.Items;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<PaginatedItemsResponse<CatalogBrandDto>> GetCatalogBrands();

        Task<int?> AddAsync(string brand);

        Task<bool> UpdateAsync(int id, string brand);

        Task<bool> DeleteAsync(int id);
    }
}