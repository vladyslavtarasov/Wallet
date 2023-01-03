namespace WalletAPI.ViewModels.ExpenseCategoryViewModels;

public class GetExpenseCategoryViewModel
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string AccountName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}