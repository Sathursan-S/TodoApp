import TodoList from "./components/TodoList";
import AddTaskButton from "./components/AddTaskButton.tsx";

const App: React.FC = () => {

  return (
    <div className="container mx-auto p-4 ">
      <h1 className="text-2xl font-bold mb-4">Todo App</h1>
      <AddTaskButton />
      <TodoList />
    </div>
  );
};

export default App;