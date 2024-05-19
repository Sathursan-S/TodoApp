import axios from "axios";
import type { TodoCreateRequest, TodoTask } from "../types/Todo";

const api = axios.create({ baseURL: 'http://localhost:3001' });

export const createTodoTask = async (todoTask : TodoCreateRequest) : Promise<TodoTask> => {
    const response = await api.post<TodoTask>('api/v1/Todo', todoTask);
    return response.data;
} 

export const getTodoTasks = async () : Promise<TodoTask[]> => {
    const response = await api.get<TodoTask[]>('api/v1/Todo');
    return response.data;
}

export const getTodoTaskById = async (id : number) : Promise<TodoTask> => {
    const response = await api.get<TodoTask>(`api/v1/Todo/${id}`);
    return response.data;
}

export const updateTodoTask = async (todoTask : TodoTask) : Promise<TodoTask> => {
    const response = await api.put<TodoTask>(`api/v1/Todo/${todoTask.id}`, todoTask);
    return response.data;
}

export const deleteTodoTask = async (id : number) : Promise<void> => {
    await api.delete(`api/v1/Todo/${id}`);
}

export const completeTodoTask = async (id : number) : Promise<TodoTask> => {
    const response = await api.put<TodoTask>(`api/v1/Todo/${id}/complete`);
    return response.data;
}