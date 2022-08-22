using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using YB.Todo.AppContext;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace YB.Todo.DesignTime
{
    public class AppContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .Build();

            var defaultMsSqlConnection = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(defaultMsSqlConnection);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
