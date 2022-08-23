using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using YB.Todo.AppContext;
using YB.Todo.Contracts;
using YB.Todo.Data;
using YB.Todo.Repositories;
using YB.Todo.Services;

namespace YB.Todo.TestProject;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbname;

    public CustomWebApplicationFactory(string dbname)
    {
        _dbname = dbname;
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's ApplicationDbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add a database context using an in-memory 
            // database for testing.
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(_dbname ?? "InMemoryTestDb")
                       .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            services.AddScoped<IToDoRepository, ToDoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IToDoService, ToDoService>();

            // Build the service provider
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            // context (ApplicationDbContext).
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationDbContext>();

                // Ensure the database is created.
                context.Database.EnsureCreated();
            }
        })
        .UseEnvironment("Test");
    }
}
