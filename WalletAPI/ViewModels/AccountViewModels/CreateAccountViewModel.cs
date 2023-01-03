namespace WalletAPI.ViewModels.AccountViewModels;

public class CreateAccountViewModel
{
    public string UserName { get; set; } = null!;
    public decimal Balance { get; set; }
    public string AccountName { get; set; } = null!;
}