using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IBaseRepository<TDbModel> where TDbModel : BaseModel 
    {
        Task<TDbModel> Get(int id);
        Task<List<TDbModel>> GetAll();
        Task Add(TDbModel model);
        Task Remove(int id);
        Task<bool> Any();
    }
}
