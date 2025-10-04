using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Tests.RepositoryTests;

public class TodoRepositoryTests : IDisposable
{
    private readonly TodoDbContext _context;
    private readonly TodoRepository _repository;

    public TodoRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TodoDbContext(options);
        _repository = new TodoRepository(_context);
    }

    [Fact]
    public void CreateTodoTask_WithValidTask_ShouldCreateTask()
    {
        // Arrange
        var task = new TodoTask
        {
            Title = "Test Task",
            Description = "Test Description",
            Priority = Priority.High
        };

        // Act
        var result = _repository.CreateTodoTask(task);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Task", result.Title);
        Assert.Equal("Test Description", result.Description);
        Assert.Equal(Priority.High, result.Priority);
        Assert.False(result.IsCompleted);
    }

    [Fact]
    public void CreateTodoTask_WithDuplicateTitle_ShouldThrowException()
    {
        // Arrange
        var task1 = new TodoTask
        {
            Title = "Duplicate Task",
            Description = "First Task"
        };
        _repository.CreateTodoTask(task1);

        var task2 = new TodoTask
        {
            Title = "Duplicate Task",
            Description = "Second Task"
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _repository.CreateTodoTask(task2));
        Assert.Equal("Title already exists", exception.Message);
    }

    [Fact]
    public void GetTodoTasks_WithTasks_ShouldReturnOrderedTasks()
    {
        // Arrange
        _repository.CreateTodoTask(new TodoTask
        {
            Title = "Task 1",
            Description = "Description 1",
            Priority = Priority.Low,
            IsCompleted = false
        });

        _repository.CreateTodoTask(new TodoTask
        {
            Title = "Task 2",
            Description = "Description 2",
            Priority = Priority.High,
            IsCompleted = false
        });

        _repository.CreateTodoTask(new TodoTask
        {
            Title = "Task 3",
            Description = "Description 3",
            Priority = Priority.Medium,
            IsCompleted = true
        });

        // Act
        var result = _repository.GetTodoTasks();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        // Verify ordering: uncompleted tasks first, ordered by priority desc
        Assert.Equal("Task 2", result[0].Title); // High priority, not completed
        Assert.Equal("Task 1", result[1].Title); // Low priority, not completed
        Assert.Equal("Task 3", result[2].Title); // Completed
    }

    [Fact]
    public void GetTodoTasks_WithNoTasks_ShouldThrowException()
    {
        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _repository.GetTodoTasks());
        Assert.Equal("No tasks found", exception.Message);
    }

    [Fact]
    public void GetTodoTask_WithValidId_ShouldReturnTask()
    {
        // Arrange
        var task = _repository.CreateTodoTask(new TodoTask
        {
            Title = "Test Task",
            Description = "Test Description"
        });

        // Act
        var result = _repository.GetTodoTask(task.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(task.Id, result.Id);
        Assert.Equal("Test Task", result.Title);
    }

    [Fact]
    public void GetTodoTask_WithInvalidId_ShouldThrowException()
    {
        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _repository.GetTodoTask(999));
        Assert.Equal("Task not found", exception.Message);
    }

    [Fact]
    public void UpdateTodoTask_WithValidId_ShouldUpdateTask()
    {
        // Arrange
        var task = _repository.CreateTodoTask(new TodoTask
        {
            Title = "Original Title",
            Description = "Original Description",
            Priority = Priority.Low
        });

        var updatedTask = new TodoTask
        {
            Title = "Updated Title",
            Description = "Updated Description",
            Priority = Priority.High,
            IsCompleted = true
        };

        // Act
        var result = _repository.UpdateTodoTask(task.Id, updatedTask);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Title", result.Title);
        Assert.Equal("Updated Description", result.Description);
        Assert.Equal(Priority.High, result.Priority);
        Assert.True(result.IsCompleted);
    }

    [Fact]
    public void UpdateTodoTask_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        var task = new TodoTask
        {
            Title = "Test Task",
            Description = "Test Description"
        };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _repository.UpdateTodoTask(999, task));
        Assert.Equal("Task not found", exception.Message);
    }

    [Fact]
    public void DeleteTodoTask_WithValidId_ShouldDeleteTask()
    {
        // Arrange
        var task = _repository.CreateTodoTask(new TodoTask
        {
            Title = "Task to Delete",
            Description = "This will be deleted"
        });

        // Act
        _repository.DeleteTodoTask(task.Id);

        // Assert
        var exception = Assert.Throws<Exception>(() => _repository.GetTodoTask(task.Id));
        Assert.Equal("Task not found", exception.Message);
    }

    [Fact]
    public void DeleteTodoTask_WithInvalidId_ShouldThrowException()
    {
        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _repository.DeleteTodoTask(999));
        Assert.Equal("Task not found", exception.Message);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
