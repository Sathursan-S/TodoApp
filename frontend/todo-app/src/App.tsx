import TodoList from "./components/TodoList";
import AddTaskButton from "./components/AddTaskButton.tsx";
import { Toaster } from 'react-hot-toast';

const App: React.FC = () => {
    return (
        <div className="container mx-auto p-4">
            <Toaster
                position="top-center"
                reverseOrder={false}
            />
            <h1 className="text-4xl font-bold mb-8 text-center">Todo App</h1>
            <div className="mb-4 flex justify-center">
                <AddTaskButton />
            </div>
            <TodoList />
        </div>
    );
};

export default App;