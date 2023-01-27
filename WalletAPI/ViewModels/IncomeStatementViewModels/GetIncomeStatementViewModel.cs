namespace WalletAPI.ViewModels.IncomeStatementViewModels;

public class GetIncomeStatementViewModel
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
}