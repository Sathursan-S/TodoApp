import axios from "axios";
import {useQuery, useMutation, useQueryClient} from 'react-query';
import {mapTodoTaskToTodoCreateRequest, TodoCreateRequest, TodoTask} from "../types/Todo";

const api = axios.create({baseURL: 'http://localhost:5053/api/v1'});

export const createTodoTask = async (todoTask: TodoCreateRequest): Promise<TodoTask> => {
    const response = await api.post<TodoTask>('/Todo', todoTask);
    return response.data;
}

export const getTodoTasks = async (): Promise<TodoTask[]> => {
    const response = await api.get<TodoTask[]>('/Todo');
    return response.data;
}

export const updateTodoTask = async (todoTask: TodoTask): Promise<TodoTask> => {
    const response = await api.put<TodoTask>(`/Todo/${todoTask.id}`, mapTodoTaskToTodoCreateRequest(todoTask));
    return response.data;
}

export const deleteTodoTask = async (id: number): Promise<void> => {
    await api.delete(`/Todo/${id}`);
}

export const completeTodoTask = async (id: number): Promise<void> => {
    await api.put(`/Todo/${id}/complete`);
}

export const useCreateTodoTask = () => {
    const queryClient = useQueryClient();
    return useMutation(createTodoTask, {
        onSuccess: () => {
            queryClient.invalidateQueries('todos');
        },
    });
};

export const useGetTodoTasks = () => {
    return useQuery('todos', getTodoTasks);
};

export const useUpdateTodoTask = () => {
    const queryClient = useQueryClient();
    return useMutation(updateTodoTask, {
        onSuccess: () => {
            queryClient.invalidateQueries('todos');
        },
    });
};

export const useDeleteTodoTask = () => {
    const queryClient = useQueryClient();
    return useMutation(deleteTodoTask, {
        onSuccess: () => {
            queryClient.invalidateQueries('todos');
        },
        onError: (error) => {
            console.error(error);
        }
    });
};

export const useCompleteTodoTask = () => {
    const queryClient = useQueryClient();
    return useMutation(completeTodoTask, {
        onSuccess: () => {
            queryClient.invalidateQueries('todos');
        },
        onError: (error) => {
            console.error(error);
        }
    });
};