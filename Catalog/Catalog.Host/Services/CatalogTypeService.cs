using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        private readonly IMapper _mapper;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedItemsResponse<CatalogTypeDto>> GetCatalogTypes()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogTypeRepository.GetAsync();

                if (result.Data.Count() == 0)
                {
                    throw new Exception($"Types not found");
                }

                return new PaginatedItemsResponse<CatalogTypeDto>()
                {
                    Data = result.Data.Select(s => _mapper.Map<CatalogTypeDto>(s)).ToList()
                };
            });
        }

        public async Task<int?> AddAsync(string brand)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _catalogTypeRepository.AddAsync(brand);
            });
        }

        public async Task<bool> UpdateAsync(int id, string type)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _catalogTypeRepository.UpdateAsync(id, type);
            });
        }

        public async Task<bool> DeleteAsync(int id)
        {
                return await ExecuteSafeAsync(async () => await _catalogTypeRepository.DeleteAsync(id));
        }
    }
}