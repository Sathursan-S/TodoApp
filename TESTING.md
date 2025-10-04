# Testing Documentation

This document provides comprehensive information about the testing strategy, setup, and execution for the Todo Application.

## Table of Contents

- [Testing Overview](#testing-overview)
- [Backend Testing](#backend-testing)
  - [Unit Tests](#backend-unit-tests)
  - [Integration Tests](#backend-integration-tests)
  - [Running Backend Tests](#running-backend-tests)
- [Frontend Testing](#frontend-testing)
  - [Unit Tests](#frontend-unit-tests)
  - [E2E Tests](#e2e-tests)
  - [Running Frontend Tests](#running-frontend-tests)
- [Test Coverage](#test-coverage)
- [Continuous Integration](#continuous-integration)

## Testing Overview

The Todo Application implements a comprehensive testing strategy covering:

- **Backend Unit Tests**: Testing individual components (Repository, Service, Controller)
- **Backend Integration Tests**: Testing the complete workflow
- **Frontend Unit Tests**: Testing utility functions and type mappings
- **Frontend E2E Tests**: Testing user workflows using Playwright

### Technology Stack

**Backend:**
- **Testing Framework**: xUnit
- **Mocking Framework**: Moq
- **In-Memory Database**: Microsoft.EntityFrameworkCore.InMemory

**Frontend:**
- **Unit Testing**: Jest with React Testing Library
- **E2E Testing**: Playwright
- **Mocking**: Axios Mock Adapter

## Backend Testing

### Backend Unit Tests

Backend unit tests are located in `backend/TodoApi.Tests/` and organized by layer:

#### Repository Tests (`RepositoryTests/TodoRepositoryTests.cs`)

Tests the data access layer with in-memory database:

- `CreateTodoTask_WithValidTask_ShouldCreateTask`
- `CreateTodoTask_WithDuplicateTitle_ShouldThrowException`
- `GetTodoTasks_WithTasks_ShouldReturnOrderedTasks`
- `GetTodoTasks_WithNoTasks_ShouldThrowException`
- `GetTodoTask_WithValidId_ShouldReturnTask`
- `GetTodoTask_WithInvalidId_ShouldThrowException`
- `UpdateTodoTask_WithValidId_ShouldUpdateTask`
- `UpdateTodoTask_WithInvalidId_ShouldThrowException`
- `DeleteTodoTask_WithValidId_ShouldDeleteTask`
- `DeleteTodoTask_WithInvalidId_ShouldThrowException`

**Total: 11 tests**

#### Service Tests (`ServiceTests/TodoServiceTests.cs`)

Tests the business logic layer using Moq to mock dependencies:

- `CreateTodoTask_WithValidRequest_ShouldReturnCreatedTask`
- `CreateTodoTask_WhenRepositoryThrowsException_ShouldPropagateException`
- `GetTodoTasks_ShouldReturnAllTasks`
- `GetTodoTasks_WhenNoTasks_ShouldThrowException`
- `GetTodoTask_WithValidId_ShouldReturnTask`
- `GetTodoTask_WithInvalidId_ShouldThrowException`
- `UpdateTodoTask_WithValidRequest_ShouldReturnUpdatedTask`
- `DeleteTodoTask_WithValidId_ShouldCallRepository`
- `DeleteTodoTask_WithInvalidId_ShouldThrowException`
- `CompleteTodoTask_WithValidId_ShouldMarkTaskAsCompleted`
- `CompleteTodoTask_WithInvalidId_ShouldThrowException`

**Total: 12 tests**

#### Controller Tests (`ControllerTests/TodoControllerTests.cs`)

Tests the API controller layer:

- `CreateTodoTask_WithValidRequest_ShouldReturnOkResult`
- `CreateTodoTask_WithInvalidRequest_ShouldReturnBadRequest`
- `GetTodoTasks_ShouldReturnAllTasks`
- `GetTodoTasks_WhenNoTasks_ShouldReturnBadRequest`
- `GetTodoTask_WithValidId_ShouldReturnTask`
- `GetTodoTask_WithInvalidId_ShouldReturnBadRequest`
- `UpdateTodoTask_WithValidRequest_ShouldReturnUpdatedTask`
- `UpdateTodoTask_WithInvalidId_ShouldReturnBadRequest`
- `DeleteTodoTask_WithValidId_ShouldReturnOk`
- `DeleteTodoTask_WithInvalidId_ShouldReturnBadRequest`
- `CompleteTodoTask_WithValidId_ShouldReturnOk`
- `CompleteTodoTask_WithInvalidId_ShouldReturnBadRequest`

**Total: 10 tests**

### Backend Integration Tests

Integration tests verify the complete workflow from service to repository:

#### Workflow Integration Tests (`IntegrationTests/TodoWorkflowIntegrationTests.cs`)

- `EndToEndWorkflow_CreateReadUpdateDelete_Success`
- `MultipleTasksWorkflow_CreationAndOrdering_Success`
- `ValidationTests_DuplicateTitle_ThrowsException`
- `ServiceLayerIntegration_MapperWorks_Success`

**Total: 4 tests**

### Running Backend Tests

```bash
# Navigate to test project
cd backend/TodoApi.Tests

# Run all tests
dotnet test

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"

# Run with code coverage
dotnet test --collect:"XPlat Code Coverage"
```

**Total Backend Tests: 37**

## Frontend Testing

### Frontend Unit Tests

Frontend unit tests are located in `frontend/todo-app/src/__tests__/`:

#### Type Utility Tests (`types/Todo.test.ts`)

Tests for type mapping and utility functions:

- `mapTodoTaskToTodoCreateRequest_ShouldMapCorrectly`
- `mapTodoTaskToTodoCreateRequest_ShouldHandleDifferentPriorities`
- `Priority_Enum_ShouldHaveCorrectValues`

**Total: 3 tests**

### E2E Tests

End-to-end tests using Playwright are located in `frontend/todo-app/e2e/`:

#### Todo Workflow Tests (`todo.spec.ts`)

Comprehensive user journey tests:

- `should display the main page with title`
- `should show add task button`
- `should open task form when add button is clicked`
- `should create a new task`
- `should complete a task`
- `should delete a task`
- `should expand task to show description`
- `should edit a task`
- `should filter tasks by priority`

**Total: 8 E2E scenarios**

### Running Frontend Tests

```bash
# Navigate to frontend project
cd frontend/todo-app

# Install dependencies (if not already done)
npm install

# Run Jest unit tests
npm test

# Run Jest tests in watch mode
npm run test:watch

# Run Jest tests with coverage
npm run test:coverage

# Install Playwright browsers (first time only)
npx playwright install

# Run E2E tests
npm run test:e2e

# Run E2E tests with UI mode
npm run test:e2e:ui

# Run E2E tests in headed mode (see browser)
npm run test:e2e:headed
```

**Total Frontend Tests: 11 (3 unit + 8 E2E)**

## Test Coverage

### Backend Coverage

The backend tests provide comprehensive coverage:

- **Repository Layer**: 100% coverage of CRUD operations
- **Service Layer**: 100% coverage of business logic
- **Controller Layer**: 100% coverage of API endpoints
- **Integration**: Full workflow coverage

### Frontend Coverage

The frontend tests cover:

- **Type Utilities**: 100% coverage of mapping functions
- **E2E Workflows**: All major user journeys

To generate coverage report:

```bash
# Backend
cd backend/TodoApi.Tests
dotnet test --collect:"XPlat Code Coverage"

# Frontend
cd frontend/todo-app
npm run test:coverage
```

## Test Execution Summary

### Quick Test Commands

```bash
# Backend - All tests
cd backend/TodoApi.Tests && dotnet test

# Frontend - Unit tests
cd frontend/todo-app && npm test

# Frontend - E2E tests  
cd frontend/todo-app && npm run test:e2e
```

### Test Results

**Backend:**
- ✅ Repository Tests: 11/11 passing
- ✅ Service Tests: 12/12 passing
- ✅ Controller Tests: 10/10 passing
- ✅ Integration Tests: 4/4 passing
- **Total: 37/37 passing**

**Frontend:**
- ✅ Unit Tests: 3/3 passing
- ✅ E2E Tests: 8 scenarios defined
- **Total: 3 passing + 8 E2E scenarios**

## Continuous Integration

For CI/CD pipelines, use the following commands:

```yaml
# Backend CI
- name: Run Backend Tests
  run: |
    cd backend/TodoApi.Tests
    dotnet test --logger "trx;LogFileName=test-results.trx"

# Frontend CI
- name: Run Frontend Unit Tests
  run: |
    cd frontend/todo-app
    npm test -- --ci --coverage

- name: Run Frontend E2E Tests
  run: |
    cd frontend/todo-app
    npx playwright install --with-deps
    npm run test:e2e
```

## Best Practices

1. **Run tests before committing**: Always run tests locally before pushing changes
2. **Write tests for new features**: Every new feature should have corresponding tests
3. **Keep tests independent**: Tests should not depend on each other
4. **Use meaningful test names**: Test names should clearly describe what is being tested
5. **Mock external dependencies**: Use mocks for external services and APIs
6. **Maintain test data**: Keep test data separate and well-organized

## Troubleshooting

### Backend Tests

**Issue**: Entity Framework in-memory database conflicts
**Solution**: Each test uses a unique database name with GUID

**Issue**: AutoMapper configuration errors
**Solution**: Use AutoMapper version 12.0.1 for compatibility

### Frontend Tests

**Issue**: Playwright browser not installed
**Solution**: Run `npx playwright install chromium`

**Issue**: Test timeouts
**Solution**: Increase timeout in `playwright.config.ts` or use `test.setTimeout()`

## Contributing

When adding new tests:

1. Follow the existing test structure and naming conventions
2. Ensure tests are independent and can run in any order
3. Add appropriate setup and teardown code
4. Document complex test scenarios
5. Update this documentation with new test information
