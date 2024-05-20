using TodoApi.Models;
using TodoApi.Models.Dto;

namespace TodoApi.Services;

public interface ITodoService
{
    TodoTask CreateTodoTask(CreateTaskRequest todoRequest);
    List<TodoTask> GetTodoTasks();
    TodoTask GetTodoTask(int id);
    TodoTask UpdateTodoTask(int id, CreateTaskRequest todoRequest);
    void DeleteTodoTask(int id);
    void CompleteTodoTask(int id);
}