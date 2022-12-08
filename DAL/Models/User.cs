namespace DAL.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;

    public List<Account> Accounts { get; set; } = new();
    public List<ExpenseCategory> ExpenseCategories { get; set; } = new();
    public List<IncomeCategory> IncomeCategories { get; set; } = new();
}