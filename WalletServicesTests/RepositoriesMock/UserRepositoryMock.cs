using DAL.Repositories.Interfaces;

namespace WalletServicesTests.RepositoriesMock;

public class UserRepositoryMock : IRepository<User>
{
    private List<User> _users = ModelsMockCreator.GetUsers();
    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public User Get(int id)
    {
        return _users.FirstOrDefault(u => u.Id.Equals(id));
    }

    public void Create(User item)
    {
        _users.Add(item);
    }

    public void Delete(int id)
    {
        var user = Get(id);
        if (user is not null)
            _users.Remove(user);
    }
    
    public void Update(User item) { }
}