using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter);

    Task<CatalogItem?> GetByIdAsync(int id);

    Task<PaginatedItems<CatalogItem>> GetByBrandAsync(string brand);

    Task<PaginatedItems<CatalogItem>> GetByTypeAsync(string type);

    Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);

    Task<bool> UpdateAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);

    Task<bool> DeleteAsync(int id);
}