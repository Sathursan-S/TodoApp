import React, {useState} from 'react';
import {FaArrowDown, FaExclamation, FaArrowUp} from 'react-icons/fa';
import {TodoCreateRequest, Priority, TodoTask} from '../types/Todo';
import {useCreateTodoTask, useUpdateTodoTask} from "../services/TodoApiService.ts";

interface TaskFormProps {
    task?: TodoTask;
    isEditing?: boolean;
    onUpdateSuccess?: () => void;
}

const TaskForm: React.FC<TaskFormProps> = ({task, isEditing, onUpdateSuccess}) => {
    const [title, setTitle] = useState(task?.title || '');
    const [description, setDescription] = useState(task?.description || '');
    const [priority, setPriority] = useState<Priority>(task?.priority || Priority.LOW);

    const createMutation = useCreateTodoTask();
    const updateMutation = useUpdateTodoTask();

    const handleTitleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setTitle(e.target.value);
    };

    const handleDescriptionChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setDescription(e.target.value);
    };

    const handlePriorityChange = (priority: Priority) => {
        setPriority(priority);
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (isEditing && task) {
            const editedTask: TodoTask = {
                id: task.id,
                title,
                description,
                priority,
                isCompleted: task.isCompleted,
                createdAt: task.createdAt
            };
            updateMutation.mutate(editedTask, {
                onSuccess: () => {
                    if (onUpdateSuccess) {
                        onUpdateSuccess();
                    }
                }
            });
        } else {
            const newTask: TodoCreateRequest = {
                title,
                description,
                priority,
            };
            createMutation.mutate(newTask,
                {
                    onSuccess: () => {
                        setTitle('');
                        setDescription('');
                        setPriority(Priority.LOW);
                    }
                });
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4 p-4 bg-white shadow-md rounded-lg">
            <div>
                <label className="block text-sm font-medium text-gray-700">Title</label>
                <input
                    type="text"
                    value={title}
                    onChange={handleTitleChange}
                    className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">Description</label>
                <textarea
                    value={description}
                    onChange={handleDescriptionChange}
                    className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">Priority</label>
                <div className="flex space-x-4 items-center justify-center">
                    <button
                        type="button"
                        onClick={() => handlePriorityChange(Priority.LOW)}
                        className={`p-2 rounded-full ${priority === Priority.LOW ? 'bg-green-200' : 'bg-gray-200'} flex items-center space-x-2`}
                    >
                        <FaArrowDown className="text-green-500"/>
                        <span className="text-sm pr-2">Low</span>
                    </button>
                    <button
                        type="button"
                        onClick={() => handlePriorityChange(Priority.MEDIUM)}
                        className={`p-2 rounded-full ${priority === Priority.MEDIUM ? 'bg-yellow-200' : 'bg-gray-200'} flex items-center space-x-2`}
                    >
                        <FaExclamation className="text-yellow-500"/>
                        <span className="text-sm pr-2">Medium</span>
                    </button>
                    <button
                        type="button"
                        onClick={() => handlePriorityChange(Priority.HIGH)}
                        className={`p-2 rounded-full ${priority === Priority.HIGH ? 'bg-red-200' : 'bg-gray-200'} flex items-center space-x-2`}
                    >
                        <FaArrowUp className="text-red-500"/>
                        <span className="text-sm pr-2">High</span>
                    </button>
                </div>
            </div>
            <div className="flex justify-center space-x-2">
                <button type="submit"
                        className="px-4 py-2 bg-blue-500 text-white rounded-md">{isEditing ? 'UPDATE' : 'ADD'}</button>
            </div>
        </form>
    );
};

export default TaskForm;
