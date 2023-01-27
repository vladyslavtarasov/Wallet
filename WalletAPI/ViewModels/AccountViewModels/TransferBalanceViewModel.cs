namespace WalletAPI.ViewModels.AccountViewModels;

public class TransferBalanceViewModel
{
    public string UserName { get; set; } = null!;
    public string FromAccountName { get; set; } = null!;
    public string ToAccountName { get; set; } = null!;
    public decimal Amount { get; set; }
}