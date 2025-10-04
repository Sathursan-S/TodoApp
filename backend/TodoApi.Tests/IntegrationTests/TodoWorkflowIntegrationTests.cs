using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Models.Dto;
using TodoApi.Repositories;
using TodoApi.Services;
using AutoMapper;
using TodoApi.Models.Mapper;

namespace TodoApi.Tests.IntegrationTests;

public class TodoWorkflowIntegrationTests : IDisposable
{
    private readonly TodoDbContext _context;
    private readonly TodoRepository _repository;
    private readonly TodoService _service;
    private readonly IMapper _mapper;

    public TodoWorkflowIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: "IntegrationTestDb_" + Guid.NewGuid())
            .Options;

        _context = new TodoDbContext(options);
        _repository = new TodoRepository(_context);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperProfile>();
        });
        _mapper = config.CreateMapper();
        _service = new TodoService(_repository, _mapper);
    }

    [Fact]
    public void EndToEndWorkflow_CreateReadUpdateDelete_Success()
    {
        // Create
        var createRequest = new CreateTaskRequest
        {
            Title = "Integration Test Task",
            Description = "Testing full workflow",
            Priority = Priority.High
        };

        var createdTask = _service.CreateTodoTask(createRequest);
        Assert.NotNull(createdTask);
        Assert.Equal("Integration Test Task", createdTask.Title);
        Assert.False(createdTask.IsCompleted);

        // Read All
        var allTasks = _service.GetTodoTasks();
        Assert.Single(allTasks);
        Assert.Equal(createdTask.Id, allTasks[0].Id);

        // Read Single
        var retrievedTask = _service.GetTodoTask(createdTask.Id);
        Assert.Equal(createdTask.Id, retrievedTask.Id);
        Assert.Equal("Integration Test Task", retrievedTask.Title);

        // Update
        var updateRequest = new CreateTaskRequest
        {
            Title = "Updated Integration Task",
            Description = "Updated description",
            Priority = Priority.Low
        };

        var updatedTask = _service.UpdateTodoTask(createdTask.Id, updateRequest);
        Assert.Equal("Updated Integration Task", updatedTask.Title);
        Assert.Equal("Updated description", updatedTask.Description);
        Assert.Equal(Priority.Low, updatedTask.Priority);

        // Complete
        _service.CompleteTodoTask(createdTask.Id);
        var completedTask = _service.GetTodoTask(createdTask.Id);
        Assert.True(completedTask.IsCompleted);

        // Delete
        _service.DeleteTodoTask(createdTask.Id);
        var exception = Assert.Throws<Exception>(() => _service.GetTodoTask(createdTask.Id));
        Assert.Equal("Task not found", exception.Message);
    }

    [Fact]
    public void MultipleTasksWorkflow_CreationAndOrdering_Success()
    {
        // Create multiple tasks with different priorities
        var tasks = new[]
        {
            new CreateTaskRequest { Title = "Low Priority Task", Description = "Desc 1", Priority = Priority.Low },
            new CreateTaskRequest { Title = "High Priority Task", Description = "Desc 2", Priority = Priority.High },
            new CreateTaskRequest { Title = "Medium Priority Task", Description = "Desc 3", Priority = Priority.Medium }
        };

        var createdTasks = new List<TodoTask>();
        foreach (var task in tasks)
        {
            createdTasks.Add(_service.CreateTodoTask(task));
        }

        // Verify all tasks are created
        var allTasks = _service.GetTodoTasks();
        Assert.Equal(3, allTasks.Count);

        // Verify ordering: High, Medium, Low priority (all uncompleted)
        Assert.Equal("High Priority Task", allTasks[0].Title);
        Assert.Equal("Medium Priority Task", allTasks[1].Title);
        Assert.Equal("Low Priority Task", allTasks[2].Title);

        // Complete high priority task
        _service.CompleteTodoTask(createdTasks[1].Id);

        // Verify ordering after completion: uncompleted tasks come first
        var tasksAfterCompletion = _service.GetTodoTasks();
        Assert.False(tasksAfterCompletion[0].IsCompleted); // Medium priority, uncompleted
        Assert.False(tasksAfterCompletion[1].IsCompleted); // Low priority, uncompleted
        Assert.True(tasksAfterCompletion[2].IsCompleted);  // High priority, completed
    }

    [Fact]
    public void ValidationTests_DuplicateTitle_ThrowsException()
    {
        // Create first task
        var firstTask = new CreateTaskRequest
        {
            Title = "Duplicate Title",
            Description = "First description",
            Priority = Priority.Medium
        };

        _service.CreateTodoTask(firstTask);

        // Try to create second task with same title
        var duplicateTask = new CreateTaskRequest
        {
            Title = "Duplicate Title",
            Description = "Second description",
            Priority = Priority.High
        };

        var exception = Assert.Throws<Exception>(() => _service.CreateTodoTask(duplicateTask));
        Assert.Equal("Title already exists", exception.Message);
    }

    [Fact]
    public void ServiceLayerIntegration_MapperWorks_Success()
    {
        // Test that mapper correctly converts CreateTaskRequest to TodoTask
        var createRequest = new CreateTaskRequest
        {
            Title = "Mapper Test",
            Description = "Testing AutoMapper integration",
            Priority = Priority.High
        };

        var createdTask = _service.CreateTodoTask(createRequest);

        Assert.Equal(createRequest.Title, createdTask.Title);
        Assert.Equal(createRequest.Description, createdTask.Description);
        Assert.Equal(createRequest.Priority, createdTask.Priority);
        Assert.False(createdTask.IsCompleted);
        Assert.True(createdTask.Id > 0);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
