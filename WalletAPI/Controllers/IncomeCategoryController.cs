using BLL.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using WalletAPI.ViewModels.IncomeCategoryViewModels;

namespace WalletAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class IncomeCategoryController : ControllerBase
{
    private readonly IIncomeCategoryService _incomeCategoryService;

    public IncomeCategoryController(IIncomeCategoryService incomeCategoryService)
    {
        _incomeCategoryService = incomeCategoryService;
    }
    
    //[EnableCors("WalletCorsPolicy")]
    [HttpPost("/IncomeCategory")]
    public IActionResult CreateIncomeCategory([FromForm] IncomeCategoryViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.AccountName)
            || string.IsNullOrEmpty(model.CategoryName))
            return BadRequest("Empty fields");

        try
        {
            _incomeCategoryService.CreateIncomeCategory(model.UserName, model.AccountName, model.CategoryName);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        //return Ok(model);
        return StatusCode(StatusCodes.Status201Created, model);
    }
    
    //[EnableCors("WalletCorsPolicy")]
    [HttpDelete("/IncomeCategory")]
    public IActionResult DeleteIncomeCategory([FromForm] IncomeCategoryViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.AccountName))
            return BadRequest("Empty fields");

        try
        {
            _incomeCategoryService.DeleteIncomeCategory(model.UserName, model.AccountName, model.CategoryName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }

        return NoContent();
    }
    
    //[EnableCors("WalletCorsPolicy")]
    [HttpGet("/IncomeCategory")]
    public IActionResult ExpenseCategories(string userName, string accountName)
    {
        List<BusinessIncomeCategory> categories;
        
        if (string.IsNullOrEmpty(userName)
            || string.IsNullOrEmpty(accountName))
            return BadRequest("Empty fields");

        try
        {
            categories =  _incomeCategoryService.GetAccountIncomeCategories(userName, accountName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }
        
        return !categories.Any() 
            ? Ok("No expense categories for this account") 
            : Ok(categories.Select(Mapper.CreateIncomeCategoryViewModel).ToList());
    }
}