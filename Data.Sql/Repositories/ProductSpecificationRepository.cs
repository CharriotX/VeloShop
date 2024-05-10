using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class ProductSpecificationRepository : BaseRepository<ProductSpecification>, IProductSpecificationRepository
    {
        public ProductSpecificationRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
