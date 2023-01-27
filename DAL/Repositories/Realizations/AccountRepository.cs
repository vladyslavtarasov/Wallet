using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Realizations;

public class AccountRepository : IRepository<Account>
{
    private WalletContext _db;

    public AccountRepository(WalletContext db)
    {
        _db = db;
    }

    public IEnumerable<Account> GetAll()
    {
        return _db.Accounts.Include(a => a.ExpenseStatements)
            .Include(a => a.IncomeStatements);
    }

    public Account Get(int id)
    {
        return _db.Accounts.Find(id);
    }

    public void Create(Account item)
    {
        _db.Accounts.Add(item);
    }

    public void Update(Account item)
    {
        _db.Entry(item).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        Account account = _db.Accounts.Find(id);
        if (account is not null)
            _db.Accounts.Remove(account);
    }
}