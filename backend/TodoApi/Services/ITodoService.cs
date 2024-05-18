using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    TodoTask CreateTodoTask(TodoTask todoTask);
    List<TodoTask> GetTodoTasks();
    TodoTask GetTodoTask(string id);
    TodoTask UpdateTodoTask(string id, TodoTask todoTask);
    void DeleteTodoTask(string id);
}