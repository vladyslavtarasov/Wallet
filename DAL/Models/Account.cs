namespace DAL.Models;

public class Account
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public string Name { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public List<ExpenseStatement> ExpenseStatements { get; set; } = new();
    public List<IncomeStatement> IncomeStatements { get; set; } = new();
}