using DAL.Repositories.Interfaces;

namespace WalletServicesTests.RepositoriesMock;

public class ExpenseCategoryRepositoryMock : IRepository<ExpenseCategory>
{
    private List<ExpenseCategory> _expenseCategories = ModelsMockCreator.GetExpenseCategories();
    public IEnumerable<ExpenseCategory> GetAll()
    {
        return _expenseCategories;
    }

    public ExpenseCategory Get(int id)
    {
        return _expenseCategories.FirstOrDefault(c => c.Id.Equals(id));
    }

    public void Create(ExpenseCategory item)
    {
        _expenseCategories.Add(item);
    }

    public void Delete(int id)
    {
        var category = Get(id);
        if (category is not null)
            _expenseCategories.Remove(category);
    }

    public void Update(ExpenseCategory item) { }
}