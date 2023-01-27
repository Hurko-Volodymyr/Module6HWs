using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<PaginatedItems<CatalogType>> GetAsync();

        Task<CatalogType?> GetByIdAsync(int id);

        Task<int?> AddAsync(string type);

        Task<bool> UpdateAsync(int id, string type);

        Task<bool> DeleteAsync(int id);
    }
}