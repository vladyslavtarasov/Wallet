using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Realizations;

public class UserRepository : IRepository<User>
{
    private WalletContext _db;
    public UserRepository(WalletContext db)
    {
        _db = db;
    }

    public IEnumerable<User> GetAll()
    {
        return _db.Users
            .Include(u => u.Accounts)
            .Include(u => u.ExpenseCategories)
            .Include(u => u.IncomeCategories);
    }

    public User Get(int id)
    {
        return _db.Users.Find(id);
    }

    public void Create(User item)
    {
        _db.Users.Add(item);
    }

    public void Update(User item)
    {
        _db.Entry(item).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        User user = _db.Users.Find(id);
        if (user is not null)
            _db.Users.Remove(user);
    }
}