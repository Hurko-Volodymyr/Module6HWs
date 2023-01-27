using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
    }

    public async Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return await ExecuteSafeAsync(async () =>
        {
            return await _catalogItemRepository.AddAsync(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName);
        });
    }

    public async Task<bool> UpdateAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var itemToUpdate = await _catalogItemRepository.GetByIdAsync(id);

            if (itemToUpdate == null)
            {
                return false;
            }

            itemToUpdate.Name = name;
            itemToUpdate.Description = description;
            itemToUpdate.Price = price;
            itemToUpdate.AvailableStock = availableStock;
            itemToUpdate.CatalogBrandId = catalogBrandId;
            itemToUpdate.CatalogTypeId = catalogTypeId;
            itemToUpdate.PictureFileName = pictureFileName;

            return await _catalogItemRepository.UpdateAsync(id, name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName);
        });
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var itemToDelete = await _catalogItemRepository.GetByIdAsync(id);

        if (itemToDelete == null)
        {
            return false;
        }

        var result = await _catalogItemRepository.DeleteAsync(id);

        return result;
    }
}