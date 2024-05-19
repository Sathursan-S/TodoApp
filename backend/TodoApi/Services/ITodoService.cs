using TodoApi.Models;
using TodoApi.Models.Dto;

namespace TodoApi.Services;

public interface ITodoService
{
    TodoTask CreateTodoTask(TaskCreateRequest createTodoRequest);
    List<TodoTask> GetTodoTasks();
    TodoTask GetTodoTask(int id);
    TodoTask UpdateTodoTask(int id, TodoTask todoTask);
    void DeleteTodoTask(int id);
}