import React from 'react';
import TodoItem from './TodoItem';
import {useGetTodoTasks} from "../services/TodoApiService.ts";
import { TodoTask } from '../types/Todo.ts';


const TodoList: React.FC = () => {
  const { data: todos, isLoading, isError } = useGetTodoTasks();

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>Error loading todos</div>;
  return (
    <div className="space-y-4 p-4">
      {todos && todos.length > 0 ? (
          todos?.map((todo: TodoTask) => (
              <TodoItem key={todo.id} todo={todo} />
          ))) : (
        <p className="text-gray-500">No tasks</p>
      )}
    </div>
  );
};

export default TodoList;
