import React from 'react';
import TodoItem from './TodoItem';
import { TodoListProps } from '../types/Todo';


const TodoList: React.FC<TodoListProps> = ({ todos, onToggleComplete, onDelete, onEdit }) => {
  return (
    <div className="space-y-4 p-4">
      {todos.map(todo => (
        <TodoItem
          key={todo.id}
          todo={todo}
          onToggleComplete={() => onToggleComplete(todo.id)}
          onDelete={() => onDelete(todo.id)}
          onEdit={() => onEdit(todo.id)}
        />
      ))}
    </div>
  );
};

export default TodoList;
