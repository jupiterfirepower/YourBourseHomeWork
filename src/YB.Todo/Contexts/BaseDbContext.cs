using Microsoft.EntityFrameworkCore;

namespace YB.Todo.Contexts
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
