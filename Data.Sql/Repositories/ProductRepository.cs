using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class ProductRepository : BaseRepository<Product> ,IProductRepository
    {
        public ProductRepository(WebContext webContext) : base(webContext)
        {

        }
    }
}
