using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogItemRepository> _logger;

        public CatalogItemRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogItemRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

            var itemsOnPage = await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
        }

        public async Task<PaginatedItems<CatalogItem>> GetByBrandAsync(string brand)
        {
            var result = await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand).Where(w => w.CatalogBrand!.Brand == brand)
                .ToListAsync();

            return new PaginatedItems<CatalogItem>() { Data = result };
        }

        public async Task<PaginatedItems<CatalogItem>> GetByTypeAsync(string type)
        {
            var result = await _dbContext.CatalogItems
                .Include(i => i.CatalogType).Where(w => w.CatalogType!.Type == type)
                .ToListAsync();

            return new PaginatedItems<CatalogItem>() { Data = result };
        }

        public async Task<CatalogItem?> GetByIdAsync(int id)
        {
            return await _dbContext.CatalogItems.Include(i => i.CatalogBrand).Include(i => i.CatalogType).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
        {
            var item = await _dbContext.AddAsync(new CatalogItem
            {
                CatalogBrandId = catalogBrandId,
                CatalogTypeId = catalogTypeId,
                Description = description,
                PictureFileName = pictureFileName,
                Price = price,
                Name = name,
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
        {
            var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(f => f.Id == id);

            if (item == null)
            {
                return false;
            }

            item!.CatalogBrandId = catalogBrandId;
            item!.CatalogTypeId = catalogTypeId;
            item!.Description = description;
            item!.PictureFileName = pictureFileName;
            item!.Price = price;
            item!.Name = name;

            _dbContext.Entry(item).CurrentValues.SetValues(item);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(f => f.Id == id);
            if (item == null)
            {
                return false;
            }

            _dbContext.Entry(item).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}