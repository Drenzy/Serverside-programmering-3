// CprService.cs

using System;
using System.Threading.Tasks;
using SoftwareTest.Models;

public class CprService
{
    private readonly TodolistContext _todoContext;

    public CprService(TodolistContext todoContext)
    {
        _todoContext = todoContext ?? throw new ArgumentNullException(nameof(todoContext));
    }

    public async Task AddCprToDatabase(string userId, string cprNumber)
    {
        try
        {
            // Validate if CPR number is not empty
            if (string.IsNullOrEmpty(cprNumber))
            {
                Console.WriteLine("CPR number cannot be empty.");
                return;
            }

            // Create a new CPR record
            Cpr newCpr = new Cpr
            {
                User = userId, // Use the actual user identifier
                Cprnr = cprNumber,
            };

            // Add the CPR record to the database
            _todoContext.Cprs.Add(newCpr);
            await _todoContext.SaveChangesAsync();

            Console.WriteLine("CPR added to the database successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add CPR to the database. Error: {ex.Message}");
        }
    }
}
