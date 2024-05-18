using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    
    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }
    
    public TodoTask CreateTodoTask(TodoTask todoTask)
    {
        try
        {
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

    public TodoTask GetTodoTask(string id)
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

    public TodoTask UpdateTodoTask(string id, TodoTask todoTask)
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

    public void DeleteTodoTask(string id)
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