using AutoFilterer.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        return Ok(data);
    }

    [HttpGet("GetFiltered")]
    public async Task<ActionResult<IEnumerable<ToDoItem>>> GetFiltered([FromQuery] ToDoItemFilter filter)
    {
        var data = await _service.GetListAsync();

        var result = data.AsQueryable()
                     .ApplyFilter(filter)
                     .ToList();

        return Ok(result);
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
    public async Task<ActionResult<TodoItemResult>> AddAsync([FromBody] [Required] AddToDoItem todoItem)
    {
        var id = await _service.AddAsync(todoItem);

        var result = new TodoItemResult { Id = id };

        return Created($"http://localhost:5178/Todo/{id}", result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(TodoItemResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(WebErrorResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItemResult>> UpdateAsync([FromBody] [Required] UpdateToDoItem todoItem)
    {
        var model = await _service.GetAsync(todoItem.Id);

        if (model == null)
        {
            return NotFound(new WebErrorResult(StatusCodes.Status404NotFound, "Not found", $"Entity {todoItem.Id} not found."));
        }

        var id = await _service.UpdateAsync(todoItem);

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