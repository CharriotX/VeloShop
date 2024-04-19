using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public abstract class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        protected WebContext _webContext;
        protected DbSet<TDbModel> _dbSet;
        
        protected BaseRepository(WebContext webContext)
        {
            _webContext = webContext;
            _dbSet = webContext.Set<TDbModel>();
        }

        public TDbModel Get(int id)
        {
            return _dbSet
                .Where(x => x.IsActive)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<TDbModel> GetAll()
        {
            return _dbSet
                .Where(x => x.IsActive)
                .ToList();
        }

        public void Add(TDbModel model)
        {
            _dbSet.Add(model);
            _webContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var model = Get(id);
            model.IsActive = false;
            _webContext.SaveChanges();
        }

        public bool Any()
        {
            return _dbSet
                .Where(x => x.IsActive)
                .Any();
        }
    }
}
