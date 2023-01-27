using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<PaginatedItems<CatalogBrand>> GetAsync();

        Task<CatalogBrand?> GetByIdAsync(int id);

        Task<int?> AddAsync(string brand);

        Task<bool> UpdateAsync(int id, string brand);

        Task<bool> DeleteAsync(int id);
    }
}