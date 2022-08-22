using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YB.Todo.Entities;

namespace YB.Todo.Contracts
{
    public interface IDataContext
    {
        DbSet<ToDoEntity> ToDoItems { get; set; }

        void BeginTransaction();
        void Commit();
        Task CommitAsync();
        void Rollback();
    }
}
