import { mapTodoTaskToTodoCreateRequest, Priority, TodoTask } from '../../types/Todo';

describe('Todo Type Utilities', () => {
  describe('mapTodoTaskToTodoCreateRequest', () => {
    it('should map TodoTask to TodoCreateRequest correctly', () => {
      const todoTask: TodoTask = {
        id: 1,
        title: 'Test Task',
        description: 'Test Description',
        priority: Priority.HIGH,
        isCompleted: false,
        createdAt: new Date('2024-01-01'),
      };

      const result = mapTodoTaskToTodoCreateRequest(todoTask);

      expect(result).toEqual({
        title: 'Test Task',
        description: 'Test Description',
        priority: Priority.HIGH,
      });

      // Should not include id, isCompleted, or createdAt
      expect(result).not.toHaveProperty('id');
      expect(result).not.toHaveProperty('isCompleted');
      expect(result).not.toHaveProperty('createdAt');
    });

    it('should handle different priority levels', () => {
      const lowPriorityTask: TodoTask = {
        id: 2,
        title: 'Low Priority',
        description: 'Description',
        priority: Priority.LOW,
        isCompleted: false,
        createdAt: new Date(),
      };

      const mediumPriorityTask: TodoTask = {
        id: 3,
        title: 'Medium Priority',
        description: 'Description',
        priority: Priority.MEDIUM,
        isCompleted: true,
        createdAt: new Date(),
      };

      const lowResult = mapTodoTaskToTodoCreateRequest(lowPriorityTask);
      const mediumResult = mapTodoTaskToTodoCreateRequest(mediumPriorityTask);

      expect(lowResult.priority).toBe(Priority.LOW);
      expect(mediumResult.priority).toBe(Priority.MEDIUM);
    });
  });

  describe('Priority enum', () => {
    it('should have correct values', () => {
      expect(Priority.LOW).toBe(0);
      expect(Priority.MEDIUM).toBe(1);
      expect(Priority.HIGH).toBe(2);
    });
  });
});
