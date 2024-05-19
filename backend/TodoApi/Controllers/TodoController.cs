using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Models.Dto;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;
    
    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }
    
    [HttpPost]
    public ActionResult<TodoTask> CreateTodoTask([FromBody] TaskCreateRequest createTodoRequest)
    {
        try
        {
            return _todoService.CreateTodoTask(createTodoRequest);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public ActionResult<List<TodoTask>> GetTodoTasks()
    {
        try
        {
            return _todoService.GetTodoTasks();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public ActionResult<TodoTask> GetTodoTask(int id)
    {
        try
        {
            return _todoService.GetTodoTask(id);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("{id}")]
    public ActionResult<TodoTask> UpdateTodoTask(int id, [FromBody] TodoTask todoTask)
    {
        try
        {
            return _todoService.UpdateTodoTask(id, todoTask);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteTodoTask(int id)
    {
        try
        {
            _todoService.DeleteTodoTask(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}