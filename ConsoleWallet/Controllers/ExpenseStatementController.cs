using BLL.Services.Interfaces;

namespace ConsoleWallet.Controllers;

public class ExpenseStatementController
{
    private IExpenseStatementService _expenseStatementService;

    public ExpenseStatementController(IExpenseStatementService expenseStatementService)
    {
        _expenseStatementService = expenseStatementService;
    }

    public void CreateExpenseStatement(string userName, string accountName, string categoryName)
    {
        decimal amount;
        bool dataIsCorrect;
        
        do
        {
            Console.WriteLine("Enter amount");
            dataIsCorrect = decimal.TryParse(Console.ReadLine(), out amount) 
                            && amount is >= 0 and <= int.MaxValue;
            
            if (dataIsCorrect) continue;
            
            Console.WriteLine("\nEnter correct amount.\n");
        } while (!dataIsCorrect);

        Console.WriteLine();

        try
        {
            _expenseStatementService.CreateExpenseStatement(userName, accountName, categoryName, amount);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}