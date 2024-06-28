import React, {useState} from "react";
import {toast} from "react-hot-toast";
import {FaEdit, FaTrash, FaArrowDown, FaExclamation, FaArrowUp} from 'react-icons/fa';
import {TodoItemProps, Priority} from '../types/Todo';
import {
    useCompleteTodoTask,
    useDeleteTodoTask,
} from "../services/TodoApiService.ts";
import TaskForm from "./TaskForm.tsx";


const TodoItem: React.FC<TodoItemProps> = ({todo}) => {
    const [isExpanded, setIsExpanded] = useState(false);
    const completeMutation = useCompleteTodoTask();
    const deleteMutation = useDeleteTodoTask();
    const [isEditing, setIsEditing] = useState(false);

    const handleComplete =  () => {
        completeMutation.mutate(todo.id);
    };

    const handleDelete = () => {
        toast.custom((t) => (
            <div className="max-w-md w-full bg-white shadow-lg rounded-lg
            pointer-events-auto flex flex-col ring-1 ring-black ring-opacity-5">
                <div className="flex  p-4">
                    <p className="text-sm font-medium text-gray-900">
                        <strong>Are you sure you want to delete this task?</strong>
                    </p>
                </div>
                <div className="flex border-t border-gray-200">
                    <button
                        onClick={() => {
                            deleteMutation.mutate(todo.id);
                            toast.dismiss(t.id);
                        }}
                        className="w-full border border-transparent rounded-none p-4
                        flex items-center justify-center text-sm font-medium text-red-600 hover:text-red-500
                        focus:outline-none focus:ring-2 focus:ring-red-500"
                    >
                        Confirm
                    </button>
                    <button
                        onClick={() => toast.dismiss(t.id)}
                        className="w-full border border-transparent rounded-none p-4
                        flex items-center justify-center text-sm font-medium text-indigo-600
                        hover:text-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                    >
                        Cancel
                    </button>
                </div>
            </div>
        ),{position: "top-center"});
    };

    const handleCloseEdit = () => {
        setIsEditing(false);
    }

    const handleEdit = () => {
        setIsEditing(true);
    }

    const getPriorityIcon = () => {
        switch (todo.priority) {
            case Priority.LOW:
                return <FaArrowDown className="text-green-500"/>;
            case Priority.MEDIUM:
                return <FaExclamation className="text-yellow-500"/>;
            case Priority.HIGH:
                return <FaArrowUp className="text-red-500"/>;
            default:
                return null;
        }
    };

    const handleExpand = () => setIsExpanded(!isExpanded);

    return (
        <div>
            <div
                className={`bg-gray-50 shadow-md rounded-lg px-4 py-2 m-2 grid grid-cols-1 items-center justify-between space-x-4 ${todo.isCompleted ? 'opacity-50 line-through' : ''}`}
                onClick={handleExpand}
            >
                <div className={"flex flex-row justify-between"}>
                    <div className="flex items-center content-center space-x-4">
                        <input
                            type="checkbox"
                            className="form-checkbox h-4 w-4"
                            checked={todo.isCompleted}
                            onChange={handleComplete}
                            onClick={(e) => e.stopPropagation()}
                        />
                        <p className="text-lg font-semibold">{todo.title}</p>
                    </div>
                    <div className="flex items-center space-x-2">
                        <div className="p-2">{getPriorityIcon()}</div>
                        <button className="p-2 text-blue-500" onClick={(e) => {
                            e.stopPropagation();
                            handleEdit();
                        }}><FaEdit/></button>
                        <button className="p-2 text-red-500" onClick={(e) => {
                            e.stopPropagation();
                            handleDelete();
                        }}><FaTrash/></button>
                    </div>
                </div>
                {isExpanded && (
                    <div>
                        <p className="mb-2">{todo.description}</p>
                        <p className="text-gray-500 text-sm">Created at: {todo.createdAt.toLocaleString()}</p>
                    </div>
                )}
            </div>

            {isEditing && (
                <div
                    className="fixed inset-0 flex items-center justify-center z-50"
                    onClick={handleCloseEdit}
                >
                    <div
                        className="absolute inset-0 bg-gray-600 opacity-50"
                        onClick={handleCloseEdit}
                    ></div>
                    <div
                        className="bg-transparent p-4 rounded-lg  z-10 max-w-md w-full"
                        onClick={(e) => e.stopPropagation()}
                    >
                        <TaskForm task={todo} isEditing={true} onUpdateSuccess={handleCloseEdit}/>
                    </div>
                </div>
            )}
        </div>
    );
};

export default TodoItem;
