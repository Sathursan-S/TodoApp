using AutoMapper;
using TodoApi.Models;
using TodoApi.Models.Dto;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    
    public TodoService(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }
    
    public TodoTask CreateTodoTask(TaskCreateRequest taskCreateRequest)
    {
        try
        {
            TodoTask todoTask = _mapper.Map<TodoTask>(taskCreateRequest);
            return _todoRepository.CreateTodoTask(todoTask);
        }
        catch (Exception e)
        {
            throw new Exception("Error creating task" + e.Message);
        }
    }

    public List<TodoTask> GetTodoTasks()
    {
        try
        {
            return _todoRepository.GetTodoTasks();
        }
        catch (Exception e)
        {
            throw new Exception("Error getting all tasks" + e.Message);
        }
    }

    public TodoTask GetTodoTask(int id)
    {
        try
        {
            return _todoRepository.GetTodoTask(id);
        }
        catch (Exception e)
        {
            throw new Exception("Error getting task" + e.Message);
        }
    }

    public TodoTask UpdateTodoTask(int id, TodoTask todoTask)
    {
        try
        {
            return _todoRepository.UpdateTodoTask(id, todoTask);
        }
        catch (Exception e)
        {
            throw new Exception("Error updating task" + e.Message);
        }
    }

    public void DeleteTodoTask(int id)
    {
        try
        {
            _todoRepository.DeleteTodoTask(id);
        }
        catch (Exception e)
        {
            throw new Exception("Error deleting task" + e.Message);
        }
    }
}