using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoRepository _todoRepository;

    public TodoController(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    [HttpGet]
    public ActionResult<List<TodoTask>> GetTodoTasks()
    {
        try
        {
            return Ok(_todoRepository.CetTodoTasks());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<TodoTask> GetTodoTask(string id)
    {
        try
        {
            return Ok(_todoRepository.GetTodoTask(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public ActionResult<TodoTask> CreateTodoTask([FromBody] TodoTask todoTask)
    {
        try
        {
            return Ok(_todoRepository.CreateTodoTask(todoTask));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<TodoTask> UpdateTodoTask(string id, [FromBody] TodoTask todoTask)
    {
        try
        {
            return Ok(_todoRepository.UpdateTodoTask(id, todoTask));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteTodoTask(string id)
    {
        try
        {
            _todoRepository.DeleteTodoTask(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}