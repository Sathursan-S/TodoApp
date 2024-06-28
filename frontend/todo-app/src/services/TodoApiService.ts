import axios, {AxiosError} from "axios";
import {useQuery, useMutation, useQueryClient} from 'react-query';
import {mapTodoTaskToTodoCreateRequest, TodoCreateRequest, TodoTask} from "../types/Todo";
import {toast} from "react-hot-toast";

const api = axios.create({baseURL: '/api/v1'});

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
            toast.success('Task created successfully');
        },
        onError: (error: AxiosError) => {
            console.error(error);
            toast.error('Failed to create task \n' + error.response?.data);
        }
    });
};

export const useGetTodoTasks = () => {
    return useQuery('todos', getTodoTasks,{
        onError: (error: AxiosError) => {
            toast.error('Failed to fetch tasks \n' + error.response?.data);
        }
    });
};

export const useUpdateTodoTask = () => {
    const queryClient = useQueryClient();
    return useMutation(updateTodoTask, {
        onSuccess: () => {
            queryClient.invalidateQueries('todos');
            toast.success('Task updated successfully');
        },
        onError: (error : AxiosError) => {
            console.error(error);
            toast.error('Failed to update task \n' + error.response?.data);
        }
    });
};

export const useDeleteTodoTask = () => {
    const queryClient = useQueryClient();
    return useMutation(deleteTodoTask, {
        onSuccess: () => {
            queryClient.invalidateQueries('todos');
            toast.success('Task deleted successfully');
        },
        onError: (error: AxiosError) => {
            console.error(error);
            toast.error('Failed to delete task \n' + error.response?.data);
        }
    });
};

export const useCompleteTodoTask = () => {
    const queryClient = useQueryClient();
    return useMutation(completeTodoTask, {
        onSuccess: () => {
            queryClient.invalidateQueries('todos');
            toast.success('Task completed successfully');
        },
        onError: (error: AxiosError) => {
            console.error(error);
            toast.error('Failed to complete task \n' + error.response?.data);
        }
    });
};