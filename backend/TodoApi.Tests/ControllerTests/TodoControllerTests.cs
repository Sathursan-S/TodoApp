using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Models.Dto;
using TodoApi.Services;

namespace TodoApi.Tests.ControllerTests;

public class TodoControllerTests
{
    private readonly Mock<ITodoService> _mockService;
    private readonly TodoController _controller;

    public TodoControllerTests()
    {
        _mockService = new Mock<ITodoService>();
        _controller = new TodoController(_mockService.Object);
    }

    [Fact]
    public void CreateTodoTask_WithValidRequest_ShouldReturnOkResult()
    {
        // Arrange
        var createRequest = new CreateTaskRequest
        {
            Title = "Test Task",
            Description = "Test Description",
            Priority = Priority.High
        };

        var createdTask = new TodoTask
        {
            Id = 1,
            Title = "Test Task",
            Description = "Test Description",
            Priority = Priority.High
        };

        _mockService.Setup(s => s.CreateTodoTask(createRequest)).Returns(createdTask);

        // Act
        var result = _controller.CreateTodoTask(createRequest);

        // Assert
        var actionResult = Assert.IsType<ActionResult<TodoTask>>(result);
        var returnValue = Assert.IsType<TodoTask>(actionResult.Value);
        Assert.Equal(1, returnValue.Id);
        Assert.Equal("Test Task", returnValue.Title);
        _mockService.Verify(s => s.CreateTodoTask(createRequest), Times.Once);
    }

    [Fact]
    public void CreateTodoTask_WithInvalidRequest_ShouldReturnBadRequest()
    {
        // Arrange
        var createRequest = new CreateTaskRequest
        {
            Title = "Duplicate Task",
            Description = "Test Description"
        };

        _mockService.Setup(s => s.CreateTodoTask(createRequest))
            .Throws(new Exception("Title already exists"));

        // Act
        var result = _controller.CreateTodoTask(createRequest);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Title already exists", badRequestResult.Value);
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

        _mockService.Setup(s => s.GetTodoTasks()).Returns(tasks);

        // Act
        var result = _controller.GetTodoTasks();

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<TodoTask>>>(result);
        var returnValue = Assert.IsType<List<TodoTask>>(actionResult.Value);
        Assert.Equal(2, returnValue.Count);
        _mockService.Verify(s => s.GetTodoTasks(), Times.Once);
    }

    [Fact]
    public void GetTodoTasks_WhenNoTasks_ShouldReturnBadRequest()
    {
        // Arrange
        _mockService.Setup(s => s.GetTodoTasks())
            .Throws(new Exception("No tasks found"));

        // Act
        var result = _controller.GetTodoTasks();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("No tasks found", badRequestResult.Value);
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

        _mockService.Setup(s => s.GetTodoTask(1)).Returns(task);

        // Act
        var result = _controller.GetTodoTask(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<TodoTask>>(result);
        var returnValue = Assert.IsType<TodoTask>(actionResult.Value);
        Assert.Equal(1, returnValue.Id);
        Assert.Equal("Test Task", returnValue.Title);
    }

    [Fact]
    public void GetTodoTask_WithInvalidId_ShouldReturnBadRequest()
    {
        // Arrange
        _mockService.Setup(s => s.GetTodoTask(999))
            .Throws(new Exception("Task not found"));

        // Act
        var result = _controller.GetTodoTask(999);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Task not found", badRequestResult.Value);
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

        var updatedTask = new TodoTask
        {
            Id = 1,
            Title = "Updated Task",
            Description = "Updated Description",
            Priority = Priority.Medium
        };

        _mockService.Setup(s => s.UpdateTodoTask(1, updateRequest)).Returns(updatedTask);

        // Act
        var result = _controller.UpdateTodoTask(1, updateRequest);

        // Assert
        var actionResult = Assert.IsType<ActionResult<TodoTask>>(result);
        var returnValue = Assert.IsType<TodoTask>(actionResult.Value);
        Assert.Equal(1, returnValue.Id);
        Assert.Equal("Updated Task", returnValue.Title);
    }

    [Fact]
    public void UpdateTodoTask_WithInvalidId_ShouldReturnBadRequest()
    {
        // Arrange
        var updateRequest = new CreateTaskRequest
        {
            Title = "Updated Task",
            Description = "Updated Description"
        };

        _mockService.Setup(s => s.UpdateTodoTask(999, updateRequest))
            .Throws(new Exception("Task not found"));

        // Act
        var result = _controller.UpdateTodoTask(999, updateRequest);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Task not found", badRequestResult.Value);
    }

    [Fact]
    public void DeleteTodoTask_WithValidId_ShouldReturnOk()
    {
        // Arrange
        _mockService.Setup(s => s.DeleteTodoTask(1));

        // Act
        var result = _controller.DeleteTodoTask(1);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        _mockService.Verify(s => s.DeleteTodoTask(1), Times.Once);
    }

    [Fact]
    public void DeleteTodoTask_WithInvalidId_ShouldReturnBadRequest()
    {
        // Arrange
        _mockService.Setup(s => s.DeleteTodoTask(999))
            .Throws(new Exception("Task not found"));

        // Act
        var result = _controller.DeleteTodoTask(999);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Task not found", badRequestResult.Value);
    }

    [Fact]
    public void CompleteTodoTask_WithValidId_ShouldReturnOk()
    {
        // Arrange
        _mockService.Setup(s => s.CompleteTodoTask(1));

        // Act
        var result = _controller.CompleteTodoTask(1);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        _mockService.Verify(s => s.CompleteTodoTask(1), Times.Once);
    }

    [Fact]
    public void CompleteTodoTask_WithInvalidId_ShouldReturnBadRequest()
    {
        // Arrange
        _mockService.Setup(s => s.CompleteTodoTask(999))
            .Throws(new Exception("Task not found"));

        // Act
        var result = _controller.CompleteTodoTask(999);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Task not found", badRequestResult.Value);
    }
}
