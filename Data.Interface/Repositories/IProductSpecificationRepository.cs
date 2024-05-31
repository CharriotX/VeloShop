using Data.Interface.DataModels.Specifications;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IProductSpecificationRepository : IBaseRepository<ProductSpecification>
    {
        Task AddSpecificationsToProduct(List<ProductSpecificationData> data, int productId);
    }
}
