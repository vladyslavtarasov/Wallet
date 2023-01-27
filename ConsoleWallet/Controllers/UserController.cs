using BLL.Services.Interfaces;

namespace ConsoleWallet.Controllers;

public class UserController
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    public string Register()
    {
        bool dataIsCorrect;
        string name, surname, userName, password;
        do
        {
            dataIsCorrect = true;
            Console.WriteLine("Enter your name");
            name = Console.ReadLine();
            if (name?.Length is > 0 and <= 30) continue;
            dataIsCorrect = false;
            Console.WriteLine("Enter correct name");
        } while (!dataIsCorrect);
        
        Console.WriteLine();
        
        do
        {
            dataIsCorrect = true;
            Console.WriteLine("Enter your surname");
            surname = Console.ReadLine();
            if (surname?.Length is > 0 and <= 30) continue;
            dataIsCorrect = false;
            Console.WriteLine("Enter correct surname");
        } while (!dataIsCorrect);
        
        Console.WriteLine();
        
        do
        {
            dataIsCorrect = true;
            Console.WriteLine("Enter your user name");
            userName = Console.ReadLine();
            if (userName?.Length is > 0 and <= 30) continue;
            dataIsCorrect = false;
            Console.WriteLine("Enter correct user name");
        } while (!dataIsCorrect);
        
        Console.WriteLine();
        
        do
        {
            dataIsCorrect = true;
            Console.WriteLine("Enter your password");
            password = Console.ReadLine();
            if (password?.Length is > 0 and <= 30) continue;
            dataIsCorrect = false;
            Console.WriteLine("Enter correct password");
        } while (!dataIsCorrect);
        
        Console.WriteLine();

        try
        {
            _userService.RegisterUser(name, surname, userName, password);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("This user already exists");
            Console.WriteLine();
            return null;
        }
        
        Console.WriteLine("\nYou have registered and logged in\n");
        Console.WriteLine($"Hello {name} {surname}\n");
        return userName;
    }

    public string Login()
    {
        bool dataIsCorrect;
        string userName, password; 
        
        do
        {
            dataIsCorrect = true;
            Console.WriteLine("Enter your user name");
            userName = Console.ReadLine();
            if (userName is not null && userName.Length is > 0 and <= 30) continue;
            dataIsCorrect = false;
            Console.WriteLine("Enter correct user name");
        } while (!dataIsCorrect);

        Console.WriteLine();
        
        do
        {
            dataIsCorrect = true;
            Console.WriteLine("Enter your password");
            password = Console.ReadLine();
            if (password is not null && password.Length is > 0 and <= 30) continue;
            dataIsCorrect = false;
            Console.WriteLine("Enter correct password");
        } while (!dataIsCorrect);

        try
        {
            _userService.Login(userName, password);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Wrong user name or password");
            Console.WriteLine();
            return null;
        }
        
        Console.WriteLine("\nYou have logged in\n");
        Console.WriteLine($"Hello {GetName(userName, password)} {GetSurname(userName, password)}\n");
        return userName;
    }

    public string Logout()
    {
        Console.WriteLine("You have logged out");
        return null;
    }

    public string GetName(string userName, string password)
    {
        return _userService.GetName(userName, password);
    }
    
    public string GetSurname(string userName, string password)
    {
        return _userService.GetSurname(userName, password);
    }
}