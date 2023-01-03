namespace BLL.Services.Interfaces;

public interface IUserService
{
    public bool RegisterUser(string name, string surname, string userName, string password);
    public bool Login(string userName, string password);
    public string GetName(string userName, string password);
    public string GetSurname(string userName, string password);
}