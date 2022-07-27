using Ulmo.Core.EFContext;
using Ulmo.Core.Entities.Base;
using Ulmo.Core.Factory;
using Ulmo.Core.Repositories.Base;

namespace Ulmo.Core.UoW
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDatabaseContext dbContext;

        private Dictionary<Type, object> repos;

        public UnitOfWork(IContextFactory contextFactory)
        {
            dbContext = contextFactory.DbContext;
        }

        public IRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>() where TEntity : Entity<TPrimaryKey>
        {
            if (repos == null)
            {
                repos = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repos.ContainsKey(type))
            {
                repos[type] = new Repository<TEntity, TPrimaryKey>(dbContext);
            }

            var repo = repos[type];

            return (IRepository<TEntity, TPrimaryKey>)repo;
        }

        public IRepository<TEntity,int> GetRepository<TEntity>() where TEntity : Entity<int>
        {
            if (repos == null)
            {
                repos = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repos.ContainsKey(type))
            {
                repos[type] = new Repository<TEntity,int>(dbContext);
            }

            return (IRepository<TEntity, int>)repos[type];
        }


        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int Commit()
        {
            return dbContext.SaveChanges();
        }
        public async Task<int> CommitAsync()
        {
            return await dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(obj: this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }
        }
    }
}
