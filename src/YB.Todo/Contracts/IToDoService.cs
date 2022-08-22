using System.Collections.Generic;
using System.Threading.Tasks;
using YB.Todo.DtoModels;
using YB.Todo.Entities;

namespace YB.Todo.Contracts
{
    public interface IToDoService
    {
        Task<IList<ToDoItem>> GetListAsync();

        Task<ToDoItem> GetAsync(int id);

        Task<int> AddAsync(ToDoItem todoItem);

        Task<bool> DeleteAsync(int id);
    }
}
