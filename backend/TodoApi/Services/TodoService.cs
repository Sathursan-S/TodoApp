﻿using AutoMapper;
using TodoApi.Models;
using TodoApi.Models.Dto;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    
    public TodoService(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }
    
    public TodoTask CreateTodoTask(CreateTaskRequest createTaskRequest)
    {
        try
        {
            TodoTask todoTask = _mapper.Map<TodoTask>(createTaskRequest);
            return _todoRepository.CreateTodoTask(todoTask);
        }
        catch (Exception e)
        {
            throw new Exception( e.Message);
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
            throw new Exception( e.Message);
        }
    }

    public TodoTask GetTodoTask(int id)
    {
        try
        {
            return _todoRepository.GetTodoTask(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public TodoTask UpdateTodoTask(int id, CreateTaskRequest todoRequest)
    {
        try
        {
            TodoTask todoTask = _mapper.Map<TodoTask>(todoRequest);
            return _todoRepository.UpdateTodoTask(id, todoTask);
        }
        catch (Exception e)
        {
            throw new Exception( e.Message);
        }
    }

    public void DeleteTodoTask(int id)
    {
        try
        {
            _todoRepository.DeleteTodoTask(id);
        }
        catch (Exception e)
        {
            throw new Exception( e.Message);
        }
    }
    
    public void CompleteTodoTask(int id)
    {
        try
        {
            TodoTask todoTask = _todoRepository.GetTodoTask(id);
            todoTask.IsCompleted = true;
            _todoRepository.UpdateTodoTask(id, todoTask);
        }
        catch (Exception e)
        {
            throw new Exception( e.Message);
        }
    }
}