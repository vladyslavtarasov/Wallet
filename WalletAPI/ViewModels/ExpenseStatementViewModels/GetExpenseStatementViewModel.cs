namespace WalletAPI.ViewModels.ExpenseStatementViewModels;

public class GetExpenseStatementViewModel
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
}