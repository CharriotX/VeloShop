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

        public async Task<TDbModel> Get(int id)
        {
            return await _dbSet
                .Where(x => x.IsActive)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TDbModel>> GetAll()
        {
            return await _dbSet
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task Add(TDbModel model)
        {
            await _dbSet.AddAsync(model);
            await _webContext.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var model = await Get(id);
            model.IsActive = false;
            await _webContext.SaveChangesAsync();
        }

        public async Task<bool> Any()
        {
            return await _dbSet
                .Where(x => x.IsActive)
                .AnyAsync();
        }
    }
}
