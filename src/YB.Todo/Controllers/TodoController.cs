using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using YB.Todo.Contracts;
using YB.Todo.DtoModels;
using YB.Todo.Models;

namespace YB.Todo.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class TodoController : ControllerBase
{
    private readonly IToDoService _service;
    private readonly ILogger<TodoController> _logger;

    public TodoController(IToDoService service, ILogger<TodoController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ToDoItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ToDoItem>>> GetListAsync()
    {
        var data = await _service.GetListAsync();

        _logger.LogInformation($"{nameof(GetListAsync)} data.Count - {data.Count}");

        return Ok(data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ToDoItem), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(WebErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ToDoItem>> Get(int id)
    {
        var model = await _service.GetAsync(id);

        if (model == null)
        {
            return NotFound(new WebErrorResult(StatusCodes.Status404NotFound, "Not found", $"Entity {id} not found."));
        }

        return model;
    }

    [HttpPost]
    [ProducesResponseType(typeof(TodoItemResult), StatusCodes.Status201Created)]
    public async Task<ActionResult<TodoItemResult>> AddAsync([FromBody] AddToDoItem todoItem)
    {
        _logger.LogInformation($"{nameof(AddAsync)} call.");

        var id = await _service.AddAsync(todoItem);

        _logger.LogInformation($"{nameof(AddAsync)} Id - {id}");

        var result = new TodoItemResult { Id = id };

        return Created($"http://localhost:5178/Todo/{id}", result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(TodoItemResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(WebErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItemResult>> UpdateAsync([FromBody] UpdateToDoItem todoItem)
    {
        var model = await _service.GetAsync(todoItem.Id);

        if (model == null)
        {
            return NotFound(new WebErrorResult(StatusCodes.Status404NotFound, "Not found", $"Entity {todoItem.Id} not found."));
        }

        _logger.LogInformation($"{nameof(UpdateAsync)} call.");

        var id = await _service.UpdateAsync(todoItem);

        _logger.LogInformation($"{nameof(UpdateAsync)} Id - {id}");

        var result = new TodoItemResult { Id = id };

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(TodoItemResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(WebErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItemResult>> DeleteAsync(int id)
    {
        var success = await _service.DeleteAsync(id);

        var result = new TodoItemResult { Id = id };

        if (!success)
        {
            return NotFound(new WebErrorResult(StatusCodes.Status404NotFound, "Not found", $"Entity {id} not found."));
        }

        return result;
    }
}