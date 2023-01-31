using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogItemRepository> _logger;

        public CatalogBrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogItemRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<PaginatedItems<CatalogBrand>> GetAsync()
        {
            var result = await _dbContext.CatalogBrands
                .ToListAsync();

            return new PaginatedItems<CatalogBrand>() { Data = result };
        }

        public async Task<CatalogBrand?> GetByIdAsync(int id)
        {
            return await _dbContext.CatalogBrands.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<int?> AddAsync(string brand)
        {
            var item = await _dbContext.AddAsync(new CatalogBrand
            {
                Brand = brand
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, string brand)
        {
            var item = await GetByIdAsync(id);
            var status = false;

            if (item != null)
            {
                    _dbContext.CatalogBrands.Update(item);
                    await _dbContext.SaveChangesAsync();
                    status = true;
            }

            return status;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _dbContext.CatalogBrands.FirstOrDefaultAsync(f => f.Id == id);
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
