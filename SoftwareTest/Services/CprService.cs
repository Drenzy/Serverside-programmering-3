// CprService.cs
using System;
using System.Threading.Tasks;
using SoftwareTest.Codes;
using SoftwareTest.Models;

public class CprService
{
    private readonly TodolistContext _todoContext;
    private readonly HashingHandlers _hashingHandlers;

    public CprService(TodolistContext todoContext, HashingHandlers hashingHandlers)
    {
        _todoContext = todoContext ?? throw new ArgumentNullException(nameof(todoContext));
        _hashingHandlers = hashingHandlers ?? throw new ArgumentNullException(nameof(hashingHandlers));
    }

    public async Task AddCprToDatabase(string userId, string cprNumber, ReturnTypes returnType)
    {
        try
        {
            // Validate if CPR number is not empty
            if (string.IsNullOrEmpty(cprNumber))
            {
                Console.WriteLine("CPR number cannot be empty.");
                return;
            }

            // Hash the CPR number before storing it
            string hashedCpr = _hashingHandlers.HMACHasing(cprNumber, returnType);

            // Create a new CPR record
            Cpr newCpr = new Cpr
            {
                User = userId, // Use the actual user identifier
                Cprnr = hashedCpr, // Store the hashed CPR number
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
