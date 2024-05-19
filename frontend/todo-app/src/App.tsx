import { useState } from "react";
import TaskForm from "./components/TaskForm";
import TodoList from "./components/TodoList";
import { TodoTask, Priority } from './types/Todo';

const App: React.FC = () => {
  const [todos, setTodos] = useState<TodoTask[]>([
    { id: 1, title: 'Task 1', description: 'Description 1', isCompleted: false, createdAt: new Date(), priority: Priority.LOW },
    { id: 2, title: 'Task 2', description: 'Description 2', isCompleted: true, createdAt: new Date(), priority: Priority.MEDIUM },
    { id: 3, title: 'Task 3', description: 'Description 3', isCompleted: false, createdAt: new Date(), priority: Priority.HIGH },
  ]);

  const handleToggleComplete = (id: number) => {
    setTodos(todos.map(todo =>
      todo.id === id ? { ...todo, isCompleted: !todo.isCompleted } : todo
    ));
  };

  const handleDelete = (id: number) => {
    setTodos(todos.filter(todo => todo.id !== id));
  };

  const handleEdit = (id: number) => {
    // TODO: Implement edit functionality
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Todo App</h1>
      <TaskForm />
      <TodoList 
        todos={todos}
        onToggleComplete={handleToggleComplete}
        onDelete={handleDelete}
        onEdit={handleEdit}
      />
    </div>
  );
};

export default App;

