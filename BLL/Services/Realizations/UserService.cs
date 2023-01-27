using BLL.Services.Interfaces;
using DAL;
using DAL.Models;

namespace BLL.Services.Realizations;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool RegisterUser(string name, string surname, string userName, string password)
    {
        var user = _unitOfWork.Users.GetAll()
            .FirstOrDefault(u => u.UserName.Equals(userName));
        if (user is not null)
            throw new ArgumentException("This user already exists", nameof(userName));

        user = new User
        {
            Name = name,
            Surname = surname,
            UserName = userName,
            Password = password
        };

        _unitOfWork.Users.Create(user);
        _unitOfWork.Save();

        return true;
    }
    
    public bool Login(string userName, string password)
    {
        var user = _unitOfWork.Users.GetAll()
            .FirstOrDefault(u => u.UserName.Equals(userName));
        if (user is null)
            throw new ArgumentException("This user does not exist", nameof(userName));

        if (!user.Password.Equals(password))
            throw new ArgumentException("This password is wrong", nameof(password));

        return true;
    }

    public string GetName(string userName, string password)
    {
        var user = _unitOfWork.Users.GetAll()
            .FirstOrDefault(u => u.UserName.Equals(userName));
        if (user is null)
            throw new ArgumentException("This user does not exist", nameof(userName));

        if (!user.Password.Equals(password))
            throw new ArgumentException("This password is wrong", nameof(password));

        return user.Name;
    }
    
    public string GetSurname(string userName, string password)
    {
        var user = _unitOfWork.Users.GetAll()
            .FirstOrDefault(u => u.UserName.Equals(userName));
        if (user is null)
            throw new ArgumentException("This user does not exist", nameof(userName));

        if (!user.Password.Equals(password))
            throw new ArgumentException("This password is wrong", nameof(password));

        return user.Surname;
    }
}