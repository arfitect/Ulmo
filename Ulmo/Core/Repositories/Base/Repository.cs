using Microsoft.EntityFrameworkCore;
using Ulmo.Core.EFContext;
using Ulmo.Core.Entities.Base;

namespace Ulmo.Core.Repositories.Base
{
    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        //private readonly DbContext _dbContext;
        private readonly IDatabaseContext _dbContext;

        //public Repository(DbContext dbContext)
        public Repository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            //await _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {

            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(TPrimaryKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            //await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TPrimaryKey id)
        {
            var entity = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            //await _dbContext.SaveChangesAsync();
        }
    }

    public class Repository<TEntity> : Repository<TEntity, int> where TEntity : Entity<int>
    {
        //public Repository(DbContext dbContext) : base(dbContext)
        public Repository(IDatabaseContext dbContext) : base(dbContext)
        {

        }
    }
}
