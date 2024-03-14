using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftwareTest.Models;

public class TodoListService
{
    private readonly TodolistContext _todoContext;

    public TodoListService(TodolistContext todoContext)
    {
        _todoContext = todoContext ?? throw new ArgumentNullException(nameof(todoContext));
    }

    public async Task<List<TodolostTb>> GetAllTodoList()
    {
        try
        {
            // Retrieve all TodoList items from the database
            return await _todoContext.TodolostTbs.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to get TodoList items from the database. Error: {ex.Message}");
            return null;
        }
    }

    public async Task AddItemToTodoList(string userId, string newItem)
    {
        try
        {
            // Validate if the new item is not empty
            if (string.IsNullOrEmpty(newItem))
            {
                Console.WriteLine("Item cannot be empty.");
                return;
            }

            // Retrieve the user based on the provided userId
            var user = await _todoContext.Cprs.FirstOrDefaultAsync(c => c.User == userId);

            // Ensure the user exists
            if (user != null)
            {
                // Create a new TodoList record
                TodolostTb newTodoItem = new TodolostTb
                {
                    Userid = user.Id, // Use the actual user identifier
                    Items = newItem,
                };

                // Add the TodoList record to the database
                _todoContext.TodolostTbs.Add(newTodoItem);
                await _todoContext.SaveChangesAsync();

                Console.WriteLine("Item added to the TodoList successfully.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add item to the TodoList. Error: {ex.Message}");
        }
    }

    public async Task DeleteItemFromTodoList(int itemId)
    {
        try
        {
            // Find the todo list item by its unique identifier
            var itemToDelete = await _todoContext.TodolostTbs.FindAsync(itemId);

            // Ensure the item exists
            if (itemToDelete != null)
            {
                // Remove the item from the context
                _todoContext.TodolostTbs.Remove(itemToDelete);

                // Save changes to the database
                await _todoContext.SaveChangesAsync();

                Console.WriteLine("Item deleted from the TodoList successfully.");
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete item from the TodoList. Error: {ex.Message}");
        }
    }

    public async Task<List<TodolostTb>> GetTodoListItems(string userId)
    {
        return await _todoContext.TodolostTbs
            .Where(t => t.User.User == userId)
            .ToListAsync();
    }
}
