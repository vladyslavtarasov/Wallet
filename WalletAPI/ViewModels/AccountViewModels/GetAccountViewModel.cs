namespace WalletAPI.ViewModels.AccountViewModels;

public class GetAccountViewModel
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public string Name { get; set; } = null!;
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
}