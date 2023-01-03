namespace WalletAPI.ViewModels.ExpenseStatementViewModels;

public class CreateExpenseStatementViewModel
{
    public string UserName { get; set; } = null!;
    public string AccountName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public decimal Amount { get; set; }
}