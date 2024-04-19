using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IBaseRepository<TDbModel> where TDbModel : BaseModel 
    {
        TDbModel Get(int id);
        List<TDbModel> GetAll();
        void Add(TDbModel model);
        void Remove(int id);
        bool Any();
    }
}
