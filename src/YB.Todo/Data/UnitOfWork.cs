using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using YB.Todo.AppContext;
using YB.Todo.Contracts;
using YB.Todo.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace YB.Todo.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public IToDoRepository ToDoRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _dbContext = context;
            _logger = logger;

            ToDoRepository = new ToDoRepository(_dbContext);
        }

        public async Task CompleteAsync()
        {
            _logger.LogInformation($"{nameof(UnitOfWork)} call CompleteAsync() method.");

            _dbContext.BeginTransaction();

            await _dbContext.CommitAsync();
        }

        public void RejectChanges()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries()
                  .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
