using System.Collections.Generic;
using System.Threading.Tasks;
using YB.Todo.AppContext;
using YB.Todo.Contracts;
using YB.Todo.Entities;

namespace YB.Todo.Repositories
{
    public class ToDoRepository : BaseRepository<ToDoEntity>, IToDoRepository
    {
        public ToDoRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public new async Task<IEnumerable<ToDoEntity>> GetAll()
        {
            return await GetAllAsync();
        }
    }
}
