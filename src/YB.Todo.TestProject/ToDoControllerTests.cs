using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using YB.Todo.DtoModels;

namespace YB.Todo.TestProject
{
    public class ToDoControllerTests
    {
        [Test]
        public async Task GetAsyncToDoListTest()
        {
            await using var application = new CustomWebApplicationFactory("InMemClear");
            using var client = application.CreateClient();

            var response = await client.GetAsync("/Todo");
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            var todoItems = JsonConvert.DeserializeObject<ToDoItem[]>(
                await response.Content.ReadAsStringAsync()
            );

            Assert.IsEmpty(todoItems);
        }

        [Test]
        public async Task GetAsyncTest()
        {
            await using var application = new CustomWebApplicationFactory("InMemToDoGetTest");
            using var client = application.CreateClient();

            var item = new AddToDoItem() { Description = "test 1", IsComplete = false };

            var response = await client.PostAsJsonAsync("/Todo", item);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);

            var result = JsonConvert.DeserializeObject<TodoItemResult>(
                await response.Content.ReadAsStringAsync()
            );

            Assert.IsTrue(result.Id > 0);

            var getResponse = await client.GetAsync($"/Todo/{result.Id}");
            Assert.IsTrue(getResponse.StatusCode == HttpStatusCode.OK);

            var todoItem = JsonConvert.DeserializeObject<ToDoItem>(
                await getResponse.Content.ReadAsStringAsync()
            );

            Assert.IsTrue(todoItem.Id > 0);


            var responseList = await client.GetAsync("/Todo");
            Assert.IsTrue(responseList.StatusCode == HttpStatusCode.OK);

            var todoItems = JsonConvert.DeserializeObject<ToDoItem[]>(
                await responseList.Content.ReadAsStringAsync()
            );

            Assert.IsNotEmpty(todoItems);
        }

        [Test]
        public async Task GetAsyncNotFoundTest()
        {
            await using var application = new CustomWebApplicationFactory("InMemToDoGetNotFoundTest");
            using var client = application.CreateClient();

            var response = await client.GetAsync($"/Todo/{-1}");

            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public async Task AddAsyncTest()
        {
            await using var application = new CustomWebApplicationFactory("InMemToDoTest");
            using var client = application.CreateClient();

            var item = new AddToDoItem() { Description = "test 1", IsComplete = false };

            var response = await client.PostAsJsonAsync("/Todo", item);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);

            var result = JsonConvert.DeserializeObject<TodoItemResult>(
                await response.Content.ReadAsStringAsync()
            );

            Assert.IsTrue(result.Id > 0);
        }

        [Test]
        public async Task UpdateAsyncTest()
        {
            await using var application = new CustomWebApplicationFactory("InMemToDoTestUpdate");
            using var client = application.CreateClient();

            var item = new AddToDoItem() { Description = "test 1", IsComplete = false };

            var response = await client.PostAsJsonAsync("/Todo", item);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);

            var result = JsonConvert.DeserializeObject<TodoItemResult>(
                await response.Content.ReadAsStringAsync()
            );

            Assert.IsTrue(result.Id > 0);

            var updateItem = new UpdateToDoItem() { Id = result.Id, Description = "test u", IsComplete = true };

            var updateResponse = await client.PutAsJsonAsync("/Todo", updateItem);

            Assert.IsTrue(updateResponse.StatusCode == HttpStatusCode.OK);

            var updateResult = JsonConvert.DeserializeObject<TodoItemResult>(
                await updateResponse.Content.ReadAsStringAsync()
            );

            Assert.IsTrue(updateResult.Id > 0);
        }


        [Test]
        public async Task DeleteAsyncTest()
        {
            await using var application = new CustomWebApplicationFactory("InMemToDoTestDeleted");
            using var client = application.CreateClient();

            var item = new AddToDoItem() { Description = "test 1", IsComplete = false };

            var response = await client.PostAsJsonAsync("/Todo", item);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);

            var result = JsonConvert.DeserializeObject<TodoItemResult>(
                await response.Content.ReadAsStringAsync()
            );

            Assert.IsTrue(result.Id > 0);

            var deleteResponse = await client.DeleteAsJsonAsync($"/Todo/{result.Id}", new { });

            Assert.IsTrue(deleteResponse.StatusCode == HttpStatusCode.OK);

            var updateResult = JsonConvert.DeserializeObject<TodoItemResult>(
                await deleteResponse.Content.ReadAsStringAsync()
            );

            Assert.IsTrue(updateResult.Id > 0);
        }

        [Test]
        public async Task DeleteAsyncNotFoundTest()
        {
            await using var application = new CustomWebApplicationFactory("InMemToDoTestNotFoundDeleted");
            using var client = application.CreateClient();

            var response = await client.DeleteAsJsonAsync($"/Todo/{-1}", new { });

            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }
    }
}
