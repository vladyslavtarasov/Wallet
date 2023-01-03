using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Realizations;

public class IncomeCategoryRepository : IRepository<IncomeCategory>
{
    private WalletContext _db;

    public IncomeCategoryRepository(WalletContext db)
    {
        _db = db;
    }

    public IEnumerable<IncomeCategory> GetAll()
    {
        return _db.IncomeCategories
            .Include(c => c.IncomeStatements);
    }

    public IncomeCategory Get(int id)
    {
        return _db.IncomeCategories.Find(id);
    }

    public void Create(IncomeCategory item)
    {
        _db.IncomeCategories.Add(item);
    }

    public void Update(IncomeCategory item)
    {
        _db.Entry(item).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        IncomeCategory category = _db.IncomeCategories.Find(id);
        if (category is not null)
            _db.IncomeCategories.Remove(category);
    }
}