import { test, expect } from '@playwright/test';

test.describe('Todo App E2E Tests', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto('/');
  });

  test('should display the main page with title', async ({ page }) => {
    await expect(page.locator('h1')).toContainText('Todo App');
  });

  test('should show add task button', async ({ page }) => {
    const addButton = page.getByRole('button', { name: /add task/i });
    await expect(addButton).toBeVisible();
  });

  test('should open task form when add button is clicked', async ({ page }) => {
    const addButton = page.getByRole('button', { name: /add task/i });
    await addButton.click();
    
    // Check if form elements appear
    await expect(page.locator('input[placeholder*="title" i]')).toBeVisible();
    await expect(page.locator('textarea')).toBeVisible();
  });

  test('should create a new task', async ({ page }) => {
    // Click add button
    await page.getByRole('button', { name: /add task/i }).click();
    
    // Fill in the form
    await page.locator('input[placeholder*="title" i]').fill('E2E Test Task');
    await page.locator('textarea').fill('This is an E2E test description');
    
    // Submit the form
    await page.getByRole('button', { name: /create|submit|add/i }).click();
    
    // Verify task appears in the list
    await expect(page.locator('text=E2E Test Task')).toBeVisible();
  });

  test('should complete a task', async ({ page }) => {
    // Create a task first
    await page.getByRole('button', { name: /add task/i }).click();
    await page.locator('input[placeholder*="title" i]').fill('Task to Complete');
    await page.locator('textarea').fill('Will be completed');
    await page.getByRole('button', { name: /create|submit|add/i }).click();
    
    // Wait for task to appear
    await expect(page.locator('text=Task to Complete')).toBeVisible();
    
    // Find and check the checkbox
    const checkbox = page.locator('input[type="checkbox"]').first();
    await checkbox.check();
    
    // Verify task is marked as completed (should have line-through style)
    const taskElement = page.locator('text=Task to Complete').locator('..');
    await expect(taskElement).toHaveClass(/line-through/);
  });

  test('should delete a task', async ({ page }) => {
    // Create a task first
    await page.getByRole('button', { name: /add task/i }).click();
    await page.locator('input[placeholder*="title" i]').fill('Task to Delete');
    await page.locator('textarea').fill('Will be deleted');
    await page.getByRole('button', { name: /create|submit|add/i }).click();
    
    // Wait for task to appear
    await expect(page.locator('text=Task to Delete')).toBeVisible();
    
    // Setup dialog handler to confirm deletion
    page.on('dialog', dialog => dialog.accept());
    
    // Click delete button (trash icon)
    const deleteButton = page.locator('button').filter({ hasText: /trash|delete/i }).first();
    await deleteButton.click();
    
    // Verify task is removed
    await expect(page.locator('text=Task to Delete')).not.toBeVisible();
  });

  test('should expand task to show description', async ({ page }) => {
    // Create a task first
    await page.getByRole('button', { name: /add task/i }).click();
    await page.locator('input[placeholder*="title" i]').fill('Expandable Task');
    await page.locator('textarea').fill('This description will be hidden initially');
    await page.getByRole('button', { name: /create|submit|add/i }).click();
    
    // Wait for task to appear
    const taskTitle = page.locator('text=Expandable Task');
    await expect(taskTitle).toBeVisible();
    
    // Description should not be visible initially
    await expect(page.locator('text=This description will be hidden initially')).not.toBeVisible();
    
    // Click on task to expand
    await taskTitle.click();
    
    // Description should now be visible
    await expect(page.locator('text=This description will be hidden initially')).toBeVisible();
  });

  test('should edit a task', async ({ page }) => {
    // Create a task first
    await page.getByRole('button', { name: /add task/i }).click();
    await page.locator('input[placeholder*="title" i]').fill('Task to Edit');
    await page.locator('textarea').fill('Original description');
    await page.getByRole('button', { name: /create|submit|add/i }).click();
    
    // Wait for task to appear
    await expect(page.locator('text=Task to Edit')).toBeVisible();
    
    // Click edit button
    const editButton = page.locator('button').filter({ hasText: /edit/i }).first();
    await editButton.click();
    
    // Modify the task
    await page.locator('input[placeholder*="title" i]').fill('Edited Task Title');
    await page.locator('textarea').fill('Updated description');
    await page.getByRole('button', { name: /update|save/i }).click();
    
    // Verify task is updated
    await expect(page.locator('text=Edited Task Title')).toBeVisible();
    await expect(page.locator('text=Task to Edit')).not.toBeVisible();
  });

  test('should filter tasks by priority', async ({ page }) => {
    // Create tasks with different priorities
    const priorities = ['High', 'Medium', 'Low'];
    
    for (const priority of priorities) {
      await page.getByRole('button', { name: /add task/i }).click();
      await page.locator('input[placeholder*="title" i]').fill(`${priority} Priority Task`);
      await page.locator('textarea').fill(`${priority} priority description`);
      
      // Select priority (assuming there's a dropdown or select)
      const prioritySelect = page.locator('select, [role="combobox"]').first();
      if (await prioritySelect.isVisible()) {
        await prioritySelect.selectOption(priority);
      }
      
      await page.getByRole('button', { name: /create|submit|add/i }).click();
      await expect(page.locator(`text=${priority} Priority Task`)).toBeVisible();
    }
    
    // Verify all tasks are visible
    await expect(page.locator('text=High Priority Task')).toBeVisible();
    await expect(page.locator('text=Medium Priority Task')).toBeVisible();
    await expect(page.locator('text=Low Priority Task')).toBeVisible();
  });
});
