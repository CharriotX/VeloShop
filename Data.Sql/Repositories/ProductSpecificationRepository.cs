using Data.Interface.DataModels.Specifications;
using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class ProductSpecificationRepository : BaseRepository<ProductSpecification>, IProductSpecificationRepository
    {
        private ISpecificationRepository _specificationRepository;
        public ProductSpecificationRepository(WebContext webContext, ISpecificationRepository specificationRepository) : base(webContext)
        {
            _specificationRepository = specificationRepository;
        }

        public async Task AddSpecificationsToProduct(List<ProductSpecificationData> data, int productId)
        {
            if(data != null)
            {
                foreach (var item in data)
                {
                    var specificationData = await _specificationRepository.GetSpecificationByName(item.Name);

                    var model = new ProductSpecification()
                    {
                        Value = item.Value,
                        SpecificationId = specificationData.Id,
                        ProductId = productId,
                    };

                    await Add(model);
                }
            }                      
        }
    }
}
