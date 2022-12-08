namespace DAL.Models;

public class IncomeCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    /*public int UserId { get; set; }
    public User User { get; set; } = null!;*/
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
    
    public List<IncomeStatement> IncomeStatements { get; set; } = new();
}