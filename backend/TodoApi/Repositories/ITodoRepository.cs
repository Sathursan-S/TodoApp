using TodoApi.Models;

namespace TodoApi.Repositories;

public interface ITodoRepository
{
    TodoTask CreateTodoTask(TodoTask todoTask);
    List<TodoTask> GetTodoTasks();
    TodoTask GetTodoTask(int id);
    TodoTask UpdateTodoTask(int id, TodoTask todoTask);
    void DeleteTodoTask(int id);
}