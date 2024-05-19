export interface TodoTask {
  id: number;
  title: string;
  description: string;
  isCompleted: boolean;
  createdAt: Date;
  priority: Priority;
}

export enum Priority {
  LOW = "LOW",
  MEDIUM = "MEDIUM",
  HIGH = "HIGH",
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
  onEdit: () => void;
  onDelete: () => void;
  onToggleComplete: () => void;
}
