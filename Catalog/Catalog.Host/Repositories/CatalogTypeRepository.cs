using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly ILogger<CatalogItemRepository> _logger;

            public CatalogTypeRepository(
                IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
                ILogger<CatalogItemRepository> logger)
            {
                _dbContext = dbContextWrapper.DbContext;
                _logger = logger;
            }

            public async Task<PaginatedItems<CatalogType>> GetAsync()
            {
                var result = await _dbContext.CatalogTypes
                    .ToListAsync();

                return new PaginatedItems<CatalogType>() { Data = result };
            }

            public async Task<CatalogType?> GetByIdAsync(int id)
            {
                return await _dbContext.CatalogTypes.FirstOrDefaultAsync(f => f.Id == id);
            }

            public async Task<int?> AddAsync(string type)
            {
                var item = await _dbContext.AddAsync(new CatalogType
                {
                    Type = type
                });

                await _dbContext.SaveChangesAsync();

                return item.Entity.Id;
            }

            public async Task<bool> UpdateAsync(int id, string type)
            {
            var item = await _dbContext.CatalogTypes.FirstOrDefaultAsync(f => f.Id == id);
            if (item == null)
            {
                return false;
            }

            item!.Id = id;
            item!.Type = type;

            _dbContext.Entry(item).CurrentValues.SetValues(item);
            await _dbContext.SaveChangesAsync();

            return true;
            }

            public async Task<bool> DeleteAsync(int id)
            {
            var brand = await _dbContext.CatalogTypes.FirstOrDefaultAsync(f => f.Id == id);
            if (brand == null)
            {
                return false;
            }

            _dbContext.Entry(brand).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();

            return true;
            }
    }
}
