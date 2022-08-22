using Microsoft.EntityFrameworkCore;
using System.Linq;
using YB.Todo.Contexts;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using YB.Todo.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using YB.Todo.Contracts;
using System.Threading.Tasks;

namespace YB.Todo.AppContext
{
    public class ApplicationDbContext : BaseDbContext, IDataContext
    {
        private IDbContextTransaction _transaction;
        public DbSet<ToDoEntity> ToDoItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(options.IsConfigured)
            {
                var optionsData = options.Options.Extensions.FirstOrDefault(ext => ext is SqlServerOptionsExtension);

                if(optionsData != null)
                    options.UseSqlServer(((SqlServerOptionsExtension)optionsData).ConnectionString);
            }

            base.OnConfiguring(options);
        }

        // On model creating function will provide us with the ability to manage the tables properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("YBTodo");
        }

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }
    }
}
