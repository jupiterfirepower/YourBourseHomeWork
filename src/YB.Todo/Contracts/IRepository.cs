using System.Threading.Tasks;

namespace YB.Todo.Contracts
{
    public interface IRepository<TEntity>
        where TEntity : class, new()
    {
        ValueTask<TEntity?> GetByIdAsync(int id);

        Task<TEntity> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
