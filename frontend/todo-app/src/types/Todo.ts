export interface TodoTask {
  id: number;
  title: string;
  description: string;
  isCompleted: boolean;
  createdAt: Date;
  priority: Priority;
}

export enum Priority {
  LOW = 0,
  MEDIUM = 1,
  HIGH = 2,
}

export interface TodoCreateRequest {
  title: string;
  description: string;
  priority: Priority;
}

export interface TodoListProps {
  todos: TodoTask[];
  onToggleComplete: (id: number) => void;
  onDelete: (id: number) => void;
  onEdit: (id: number) => void;
}

export interface TodoItemProps {
  todo: TodoTask;
}

export const mapTodoTaskToTodoCreateRequest = (todoTask: TodoTask): TodoCreateRequest => {
  return {
    title: todoTask.title,
    description: todoTask.description,
    priority: todoTask.priority,
  };
};