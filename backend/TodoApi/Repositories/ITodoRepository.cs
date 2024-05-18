using TodoApi.Models;

namespace TodoApi.Repositories;

public interface ITodoRepository
{
    TodoTask CreateTodoTask(TodoTask todoTask);
    List<TodoTask> CetTodoTasks();
    TodoTask GetTodoTask(string id);
    TodoTask UpdateTodoTask(string id, TodoTask todoTask);
    void DeleteTodoTask(string id);
}