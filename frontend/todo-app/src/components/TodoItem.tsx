import React, { useState } from "react";
import { FaEdit, FaTrash, FaArrowDown, FaExclamation, FaArrowUp } from 'react-icons/fa';
import { TodoItemProps, Priority, } from '../types/Todo';


const TodoItem: React.FC<TodoItemProps> = ({ todo, onEdit, onDelete, onToggleComplete }) => {
  const [isExpanded, setIsExpanded] = useState(false);

  const getPriorityIcon = () => {
    switch (todo.priority) {
      case Priority.LOW:
        return <FaArrowDown className="text-green-500" />;
      case Priority.MEDIUM:
        return <FaExclamation className="text-yellow-500" />;
      case Priority.HIGH:
        return <FaArrowUp className="text-red-500" />;
      default:
        return null;
    }
  };

  const handleExpand = () => setIsExpanded(true);
  const handleClose = () => setIsExpanded(false);

  return (
    <div>
      <div
        className={`bg-gray-50 shadow-md rounded-lg px-4 py-2 m-2 flex items-center justify-between space-x-4 ${todo.isCompleted ? 'opacity-50' : ''}`}
        onClick={handleExpand}
      >
        <div className="flex items-center content-center space-x-4">
          <input
            type="checkbox"
            className="form-checkbox h-4 w-4"
            checked={todo.isCompleted}
            onChange={onToggleComplete}
          />
          <p className="text-lg font-semibold">{todo.title}</p>
        </div>
        <div className="flex items-center space-x-2">
          <div className="p-2">{getPriorityIcon()}</div>
          <button className="p-2 text-blue-500" onClick={(e) => { e.stopPropagation(); onEdit(); }}><FaEdit /></button>
          <button className="p-2 text-red-500" onClick={(e) => { e.stopPropagation(); onDelete(); }}><FaTrash /></button>
        </div>
      </div>

      {isExpanded && (
        <div
          className="fixed inset-0 flex items-center justify-center z-50"
          onClick={handleClose}
        >
          <div
            className="absolute inset-0 bg-gray-600 opacity-50"
            onClick={handleClose}
          ></div>
          <div
            className="bg-white p-4 rounded-lg shadow-lg z-10 max-w-md w-full"
            onClick={(e) => e.stopPropagation()}
          >
            <div className="flex justify-between items-center mb-4">
              <h2 className="text-xl font-bold">{todo.title}</h2>
            </div>
            <p className="mb-2">{todo.description}</p>
            <p className="text-gray-500 text-sm">Created at: {todo.createdAt.toLocaleString()}</p>
          </div>
        </div>
      )}
    </div>
  );
};

export default TodoItem;
