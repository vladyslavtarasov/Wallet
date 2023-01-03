namespace WalletAPI.ViewModels.IncomeCategoryViewModels;

public class GetIncomeCategoryViewModel
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string AccountName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}