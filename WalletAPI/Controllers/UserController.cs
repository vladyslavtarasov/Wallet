using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using WalletAPI.ViewModels.UserViewModels;

namespace WalletAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    //[EnableCors("WalletCorsPolicy")]
    [HttpPost("/User/Register")]
    public IActionResult RegisterUser([FromForm] RegisterUserViewModel model)
    {
        if (string.IsNullOrEmpty(model.Name) 
            || string.IsNullOrEmpty(model.Surname)
            || string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.Password))
            return BadRequest("Empty fields");

        try
        {
            _userService.RegisterUser(model.Name, model.Surname, model.UserName, model.Password);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        //return Ok(model);
        return StatusCode(StatusCodes.Status201Created, model);
    }
    
    //[EnableCors("WalletCorsPolicy")]
    [HttpPost("/User/Login")]
    public IActionResult Login([FromForm] LoginUserViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.Password))
            return BadRequest("Empty fields");

        try
        {
            _userService.Login(model.UserName, model.Password);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        return Ok(model);
    }
}