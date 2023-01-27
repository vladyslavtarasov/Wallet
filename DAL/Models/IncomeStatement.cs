namespace DAL.Models;

public class IncomeStatement
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    
    /*public int AccountId { get; set; }
    public Account Account { get; set; } = null!;*/
    
    public int IncomeCategoryId { get; set; }
    public IncomeCategory IncomeCategory { get; set; } = null!;
}