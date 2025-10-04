
# Todo Application

## Overview

This Todo Application allows users to manage their tasks by providing functionalities to create, read, update, and delete tasks. The backend is built with C# .NET and an SQLite database, while the frontend is developed using ReactJS with TypeScript, Vite, Tailwind CSS, and React Query.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
  - [Backend Setup](#backend-setup)
  - [Frontend Setup](#frontend-setup)
- [Running the Application](#running-the-application)
- [Testing](#testing)
- [Version Control](#version-control)

## Features

- **CRUD Operations**: Create, Read, Update, and Delete tasks.
- **Priority Management**: Set priority levels for tasks.
- **Task Completion**: Mark tasks as completed.
- **Responsive UI**: User-friendly and responsive interface.
- **Real-time Updates**: Automatic updates using React Query.

## Technologies Used

- **Backend**: C# .NET, Entity Framework, SQLite, Swagger
- **Frontend**: ReactJS, TypeScript, Vite, Tailwind CSS, React Query
- **Testing**: 
  - Backend: xUnit, Moq, Entity Framework InMemory
  - Frontend: Jest, React Testing Library, Playwright
- **Version Control**: Git, GitFlow

## Setup Instructions

#### Prerequisites

- **Node.js**: Ensure you have Node.js installed. Version 20.x or higher is recommended. [Download and install Node.js](https://nodejs.org/).
- **.NET SDK**: Ensure you have the .NET SDK installed. Version 8.x or higher is recommended. [Download and install .NET SDK](https://dotnet.microsoft.com/download).

### Backend Setup

1. **Clone the repository:**

   ```bash
   git clone https://github.com/Sathursan-S/TodoApp.git
   cd TodoApp/backend/TodoApi
   ```

2. **Restore dependencies:**

   ```bash
   dotnet restore
   ```

3. **Update the database:**

   ```bash
   dotnet ef database update
   ```

4. **Run the application:**

   ```bash
   dotnet run
   ```

5. **Access the Swagger UI for API documentation:**

   Open your browser and navigate to:
   ```
   http://localhost:5053/swagger/index.html
   ```


### Frontend Setup

1. **Navigate to the frontend directory:**

   ```bash
   cd ../frontend/todo-app
   ```

2. **Install dependencies:**

   You can use npm, Yarn, pnpm, or Bun. Choose one of the following commands based on your preferred package manager:

   ```bash
   # For npm
   npm install

   # For Yarn
   yarn install

   # For pnpm
   pnpm install

   # For Bun
   bun install
   ```

3. **Run the development server:**

   Choose one of the following commands based on your preferred package manager:

   ```bash
   # For npm
   npm run dev

   # For Yarn
   yarn dev

   # For pnpm
   pnpm dev

   # For Bun
   bun dev
   ```

4. **Access the frontend application:**

   Open your browser and navigate to:
   ```
   http://localhost:5174
   ```

### Running the Application

To run the application locally, follow these steps:

1. **Start the backend:**

   In the `backend/TodoApi` directory, run:
   ```bash
   dotnet run
   ```

2. **Start the frontend:**

   In the `frontend/todo-app` directory, run the appropriate command for your package manager:

   ```bash
   # For npm
   npm run dev

   # For Yarn
   yarn dev

   # For pnpm
   pnpm dev

   # For Bun
   bun dev
   ```

3. **Open the application in your browser:**

   Frontend:
   ```
   http://localhost:5174
   ```

   Swagger API Documentation:
   ```
   http://localhost:5053/swagger/index.html
   ```

## Testing

The application includes comprehensive tests for both backend and frontend. See [TESTING.md](./TESTING.md) for detailed testing documentation.

### Quick Test Commands

**Backend Tests:**
```bash
cd backend/TodoApi.Tests
dotnet test
```

**Frontend Unit Tests:**
```bash
cd frontend/todo-app
npm test
```

**Frontend E2E Tests:**
```bash
cd frontend/todo-app
npx playwright install  # First time only
npm run test:e2e
```

### Test Coverage

- **Backend**: 37 tests (11 repository + 12 service + 10 controller + 4 integration)
- **Frontend**: 3 unit tests + 8 E2E scenarios

For more information, see the [complete testing documentation](./TESTING.md).

### Version Control

- **Repository:** [GitHub Repository](https://github.com/Sathursan-S/TodoApp)
- **Branching Strategy:** Use `main` for production, `develop` for development, and feature branches for new features.
- **Commit Messages:** Follow [Conventional Commits](https://www.conventionalcommits.org/) for clear and consistent commit messages.
