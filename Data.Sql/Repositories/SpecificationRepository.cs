using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class SpecificationRepository : BaseRepository<Specification>, ISpecificationRepository
    {
        public SpecificationRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
