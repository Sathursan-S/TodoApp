import React, { useState } from 'react';
import TaskForm from './TaskForm';

const AddTaskButton: React.FC = () => {
  const [isFormVisible, setIsFormVisible] = useState(false);

  const handleOpenForm = () => setIsFormVisible(true);
  const handleCloseForm = () => setIsFormVisible(false);

  return (
    <div>
      <button
        className="px-4 py-2 bg-red-500 text-white rounded-md"
        onClick={handleOpenForm}
      >
        Add Task
      </button>

      {isFormVisible && (
        <div
          className="fixed inset-0 flex items-center justify-center z-50 bg-transparent"
          onClick={handleCloseForm}
        >
          <div
            className="absolute inset-0 bg-gray-600 opacity-50 "
            onClick={handleCloseForm}
          ></div>
          <div
            className="bg-transparent p-4 rounded-lg z-10 max-w-md w-full"
            onClick={(e) => e.stopPropagation()}
          >
            <TaskForm isEditing={false} onUpdateSuccess={handleCloseForm}/>
            <button
              onClick={handleCloseForm}
              className="absolute top-2 right-2 text-red-500"
            >
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default AddTaskButton;
