using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using YB.Todo.AppContext;
using YB.Todo.Contracts;
using YB.Todo.Data;
using YB.Todo.DtoModels;
using YB.Todo.Mappings;
using YB.Todo.Services;

namespace YB.Todo.TestProject
{
    public class Tests
    {
        private IToDoService _service;

        private IToDoService Configure(string dbname)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: dbname)
                            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                            .Options;

            var context = new ApplicationDbContext(options);

            var logger = Mock.Of<ILogger<UnitOfWork>>();

            var unitOfWork = new UnitOfWork(context, logger);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            var mapper = config.CreateMapper();

            var service = new ToDoService(unitOfWork, mapper);

            return service;
        }

        [SetUp]
        public void Setup()
        {
            _service = Configure("yb-todo-db");
        }

        [Test]
        public async Task GetListAsyncTest()
        {
            var service = Configure("yb-todo-db-clear");
            var data = await service.GetListAsync();
            Assert.IsFalse(data.Any());
        }

        [Test]
        public async Task GetAsyncReturnNullTest()
        {
            var data = await _service.GetAsync(-1);
            Assert.IsTrue(data == null);
        }

        [Test]
        public async Task AddAsyncTest()
        {
            var item = new AddToDoItem() { Description = "test", IsComplete = false };
            var id = await _service.AddAsync(item);
            Assert.IsTrue(id > 0);

            var data = await _service.GetAsync(id);
            Assert.IsTrue(data.Id > 0);
            Assert.IsTrue(data.Description.Equals(item.Description));
            Assert.IsTrue(data.IsComplete == item.IsComplete);
            Assert.IsTrue(data.LastModifiedOnUtc == null);
        }

        [Test]
        public async Task UpdateAsyncTest()
        {
            var item = new AddToDoItem() { Description = "test", IsComplete = false };
            var id = await _service.AddAsync(item);
            Assert.IsTrue(id > 0);

            var data = await _service.GetAsync(id);
            Assert.IsTrue(data.Id > 0);
            Assert.IsTrue(data.Description.Equals(item.Description));
            Assert.IsTrue(data.IsComplete == item.IsComplete);
            Assert.IsTrue(data.LastModifiedOnUtc == null);

            var updateItem = new UpdateToDoItem() { Id = id, Description = "test 1", IsComplete = true };
            var updatedId = await _service.UpdateAsync(updateItem);
            Assert.IsTrue(updatedId > 0);

            var updatedData = await _service.GetAsync(updatedId);
            Assert.IsTrue(updatedData.Description.Equals(updateItem.Description));
            Assert.IsTrue(updatedData.IsComplete == updateItem.IsComplete);
            Assert.IsTrue(updatedData.LastModifiedOnUtc != null);
        }


        [Test]
        public async Task DeleteAsyncTest()
        {
            var item = new AddToDoItem() { Description = "test", IsComplete = false };
            var id = await _service.AddAsync(item);
            Assert.IsTrue(id > 0);

            var data = await _service.GetAsync(id);
            Assert.IsTrue(data.Id > 0);
            Assert.IsTrue(data.Description.Equals(item.Description));
            Assert.IsTrue(data.IsComplete == item.IsComplete);
            Assert.IsTrue(data.LastModifiedOnUtc == null);

            var deletedId = await _service.DeleteAsync(data.Id);
            Assert.IsTrue(deletedId);
        }

        [Test]
        public async Task DeleteAsyncReturnFalseTest()
        {
            var deletedId = await _service.DeleteAsync(-1);
            Assert.IsFalse(deletedId);
        }
    }
}