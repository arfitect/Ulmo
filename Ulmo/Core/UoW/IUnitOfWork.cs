using Ulmo.Core.Entities.Base;
using Ulmo.Core.Repositories.Base;

namespace Ulmo.Core.UoW
{
    public interface IUnitOfWork
    {
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();
        /// <returns>The number of objects in an Added, Modified, or Deleted state asynchronously</returns>
        Task<int> CommitAsync();
        /// <returns>Repository</returns>
        IRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>() where TEntity : Entity<TPrimaryKey>;

        IRepository<TEntity, int> GetRepository<TEntity>() where TEntity : Entity<int>;
    }
}
