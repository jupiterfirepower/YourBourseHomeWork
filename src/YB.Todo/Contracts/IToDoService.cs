using System.Collections.Generic;
using System.Threading.Tasks;
using YB.Todo.DtoModels;

namespace YB.Todo.Contracts
{
    public interface IToDoService
    {
        Task<IList<ToDoItem>> GetListAsync();

        Task<ToDoItem> GetAsync(int id);

        Task<int> AddAsync(AddToDoItem todoItem);

        Task<int> UpdateAsync(UpdateToDoItem todoItem);

        Task<bool> DeleteAsync(int id);
    }
}
