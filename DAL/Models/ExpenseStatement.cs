namespace DAL.Models;

public class ExpenseStatement
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    
    /*public int AccountId { get; set; }
    public Account Account { get; set; } = null!;*/
    
    public int ExpenseCategoryId { get; set; }
    public ExpenseCategory ExpenseCategory { get; set; } = null!;
}