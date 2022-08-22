using System.Collections.Generic;
using System.Threading.Tasks;
using YB.Todo.Entities;

namespace YB.Todo.Contracts
{
    public interface IToDoRepository : IRepository<ToDoEntity>
    {
        Task<IEnumerable<ToDoEntity>> GetAll();
    }
}
