using Data.Interface.DataModels.Specifications;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class SpecificationRepository : BaseRepository<Specification>, ISpecificationRepository
    {
        public SpecificationRepository(WebContext webContext) : base(webContext)
        {
        }

        public async Task<SpecificationData> GetSpecificationByName(string name)
        {
            var model = await _dbSet.FirstOrDefaultAsync(specification => specification.Name == name);
            return new SpecificationData
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
