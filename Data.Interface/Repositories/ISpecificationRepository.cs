using Data.Interface.DataModels.Specifications;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface ISpecificationRepository : IBaseRepository<Specification>
    {
        Task<SpecificationData> GetSpecificationByName(string name);
    }
}
