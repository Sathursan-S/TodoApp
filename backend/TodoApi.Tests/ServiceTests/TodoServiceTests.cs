using AutoMapper;
using Moq;
using TodoApi.Models;
using TodoApi.Models.Dto;
using TodoApi.Repositories;
using TodoApi.Services;

namespace TodoApi.Tests.ServiceTests;

public class TodoServiceTests
{
    private readonly Mock<ITodoRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly TodoService _service;

    public TodoServiceTests()
    {
        _mockRepository = new Mock<ITodoRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new TodoService(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public void CreateTodoTask_WithValidRequest_ShouldReturnCreatedTask()
    {
        // Arrange
        var createRequest = new CreateTaskRequest
        {
            Title = "Test Task",
            Description = "Test Description",
            Priority = Priority.High
        };

        var todoTask = new TodoTask
        {
            Id = 1,
            Title = "Test Task",
            Description = "Test Description",
            Priority = Priority.High
        };

        _mockMapper.Setup(m => m.Map<TodoTask>(createRequest)).Returns(todoTask);
        _mockRepository.Setup(r => r.CreateTodoTask(todoTask)).Returns(todoTask);

        // Act
        var result = _service.CreateTodoTask(createRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Task", result.Title);
        _mockRepository.Verify(r => r.CreateTodoTask(It.IsAny<TodoTask>()), Times.Once);
        _mockMapper.Verify(m => m.Map<TodoTask>(createRequest), Times.Once);
    }

    [Fact]
    public void CreateTodoTask_WhenRepositoryThrowsException_ShouldPropagateException()
    {
        // Arrange
        var createRequest = new CreateTaskRequest
        {
            Title = "Duplicate Task",
            Description = "Test Description"
        };

        var todoTask = new TodoTask
        {
            Title = "Duplicate Task",
            Description = "Test Description"
        };

        _mockMapper.Setup(m => m.Map<TodoTask>(createRequest)).Returns(todoTask);
        _mockRepository.Setup(r => r.CreateTodoTask(todoTask))
            .Throws(new Exception("Title already exists"));

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _service.CreateTodoTask(createRequest));
        Assert.Equal("Title already exists", exception.Message);
    }

    [Fact]
    public void GetTodoTasks_ShouldReturnAllTasks()
    {
        // Arrange
        var tasks = new List<TodoTask>
        {
            new TodoTask { Id = 1, Title = "Task 1", Description = "Desc 1" },
            new TodoTask { Id = 2, Title = "Task 2", Description = "Desc 2" }
        };

        _mockRepository.Setup(r => r.GetTodoTasks()).Returns(tasks);

        // Act
        var result = _service.GetTodoTasks();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _mockRepository.Verify(r => r.GetTodoTasks(), Times.Once);
    }

    [Fact]
    public void GetTodoTasks_WhenNoTasks_ShouldThrowException()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetTodoTasks())
            .Throws(new Exception("No tasks found"));

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _service.GetTodoTasks());
        Assert.Equal("No tasks found", exception.Message);
    }

    [Fact]
    public void GetTodoTask_WithValidId_ShouldReturnTask()
    {
        // Arrange
        var task = new TodoTask
        {
            Id = 1,
            Title = "Test Task",
            Description = "Test Description"
        };

        _mockRepository.Setup(r => r.GetTodoTask(1)).Returns(task);

        // Act
        var result = _service.GetTodoTask(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Task", result.Title);
        _mockRepository.Verify(r => r.GetTodoTask(1), Times.Once);
    }

    [Fact]
    public void GetTodoTask_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetTodoTask(999))
            .Throws(new Exception("Task not found"));

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _service.GetTodoTask(999));
        Assert.Equal("Task not found", exception.Message);
    }

    [Fact]
    public void UpdateTodoTask_WithValidRequest_ShouldReturnUpdatedTask()
    {
        // Arrange
        var updateRequest = new CreateTaskRequest
        {
            Title = "Updated Task",
            Description = "Updated Description",
            Priority = Priority.Medium
        };

        var todoTask = new TodoTask
        {
            Title = "Updated Task",
            Description = "Updated Description",
            Priority = Priority.Medium
        };

        var updatedTask = new TodoTask
        {
            Id = 1,
            Title = "Updated Task",
            Description = "Updated Description",
            Priority = Priority.Medium
        };

        _mockMapper.Setup(m => m.Map<TodoTask>(updateRequest)).Returns(todoTask);
        _mockRepository.Setup(r => r.UpdateTodoTask(1, todoTask)).Returns(updatedTask);

        // Act
        var result = _service.UpdateTodoTask(1, updateRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Updated Task", result.Title);
        _mockRepository.Verify(r => r.UpdateTodoTask(1, It.IsAny<TodoTask>()), Times.Once);
    }

    [Fact]
    public void DeleteTodoTask_WithValidId_ShouldCallRepository()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteTodoTask(1));

        // Act
        _service.DeleteTodoTask(1);

        // Assert
        _mockRepository.Verify(r => r.DeleteTodoTask(1), Times.Once);
    }

    [Fact]
    public void DeleteTodoTask_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteTodoTask(999))
            .Throws(new Exception("Task not found"));

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _service.DeleteTodoTask(999));
        Assert.Equal("Task not found", exception.Message);
    }

    [Fact]
    public void CompleteTodoTask_WithValidId_ShouldMarkTaskAsCompleted()
    {
        // Arrange
        var task = new TodoTask
        {
            Id = 1,
            Title = "Test Task",
            Description = "Test Description",
            IsCompleted = false
        };

        _mockRepository.Setup(r => r.GetTodoTask(1)).Returns(task);
        _mockRepository.Setup(r => r.UpdateTodoTask(1, It.IsAny<TodoTask>())).Returns(task);

        // Act
        _service.CompleteTodoTask(1);

        // Assert
        _mockRepository.Verify(r => r.GetTodoTask(1), Times.Once);
        _mockRepository.Verify(r => r.UpdateTodoTask(1, It.Is<TodoTask>(t => t.IsCompleted == true)), Times.Once);
    }

    [Fact]
    public void CompleteTodoTask_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetTodoTask(999))
            .Throws(new Exception("Task not found"));

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _service.CompleteTodoTask(999));
        Assert.Equal("Task not found", exception.Message);
    }
}
