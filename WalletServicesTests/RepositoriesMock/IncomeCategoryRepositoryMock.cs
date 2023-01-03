using DAL.Repositories.Interfaces;

namespace WalletServicesTests.RepositoriesMock;

public class IncomeCategoryRepositoryMock : IRepository<IncomeCategory>
{
    private List<IncomeCategory> _incomeCategories = ModelsMockCreator.GetIncomeCategories();
    public IEnumerable<IncomeCategory> GetAll()
    {
        return _incomeCategories;
    }

    public IncomeCategory Get(int id)
    {
        return _incomeCategories.FirstOrDefault(c => c.Id.Equals(id));
    }

    public void Create(IncomeCategory item)
    {
        _incomeCategories.Add(item);
    }

    public void Delete(int id)
    {
        var category = Get(id);
        if (category is not null)
            _incomeCategories.Remove(category);
    }

    public void Update(IncomeCategory item) { }
}