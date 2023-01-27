using DAL.Repositories.Interfaces;

namespace WalletServicesTests.RepositoriesMock;

public class AccountRepositoryMock : IRepository<Account>
{
    private List<Account> _accounts = ModelsMockCreator.GetAccount();

    public IEnumerable<Account> GetAll()
    {
        return _accounts;
    }

    public Account Get(int id)
    {
        return _accounts.FirstOrDefault(a => a.Id.Equals(id));
    }

    public void Create(Account item)
    {
        _accounts.Add(item);
    }

    public void Delete(int id)
    {
        var account = Get(id);
        if (account is not null)
            _accounts.Remove(account);
    }
    
    public void Update(Account item) { }
}