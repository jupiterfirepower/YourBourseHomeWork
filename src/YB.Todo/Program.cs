using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using YB.Todo.AppContext;
using YB.Todo.Contracts;
using YB.Todo.Data;
using YB.Todo.Repositories;
using YB.Todo.Services;
using YB.Todo.Extentions;

const string localHostOrigins = "_localHostOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPlatformMvc();

builder.Services.AddControllers();

builder.Services.AddCors(opts => opts.AddPolicy(name: localHostOrigins, policy =>
{
    policy.WithOrigins("localhost:5178", "http://localhost:5178", "localhost:7178", "https://localhost:7178", "http://localhost:3000")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowAnyOrigin();
}));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var defaultMsSqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(defaultMsSqlConnection,
                                    ef => ef.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)),
                                    ServiceLifetime.Scoped);

builder.Services.AddScoped<IDataContext>(provider => provider.GetService<ApplicationDbContext>());

builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IToDoService, ToDoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(localHostOrigins);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }