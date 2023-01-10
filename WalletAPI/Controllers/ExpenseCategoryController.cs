using BLL.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using WalletAPI.ViewModels.ExpenseCategoryViewModels;

namespace WalletAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpenseCategoryController : ControllerBase
{
    private readonly IExpenseCategoryService _expenseCategoryService;

    public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
    {
        _expenseCategoryService = expenseCategoryService;
    }
    
    //[EnableCors("WalletCorsPolicy")]
    [HttpPost("/ExpenseCategory")]
    public IActionResult CreateExpenseCategory([FromForm] ExpenseCategoryViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.AccountName)
            || string.IsNullOrEmpty(model.CategoryName))
            return BadRequest("Empty fields");

        try
        {
            _expenseCategoryService.CreateExpenseCategory(model.UserName, model.AccountName, model.CategoryName);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        //return Ok(model);
        return StatusCode(StatusCodes.Status201Created, model);
    }
    
    //[EnableCors("WalletCorsPolicy")]
    [HttpDelete("/ExpenseCategory")]
    public IActionResult DeleteExpenseCategory([FromForm] ExpenseCategoryViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.AccountName))
            return BadRequest("Empty fields");

        try
        {
            _expenseCategoryService.DeleteExpenseCategory(model.UserName, model.AccountName, model.CategoryName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }

        return NoContent();
    }
    
    //[EnableCors("WalletCorsPolicy")]
    [HttpGet("/ExpenseCategory")]
    public IActionResult ExpenseCategories(string userName, string accountName)
    {
        List<BusinessExpenseCategory> categories;
        
        if (string.IsNullOrEmpty(userName)
            || string.IsNullOrEmpty(accountName))
            return BadRequest("Empty fields");

        try
        {
            categories =  _expenseCategoryService.GetAccountExpenseCategories(userName, accountName);
        }
        catch (ArgumentException exception)
        {
            return NotFound(exception.Message);
        }
        
        return !categories.Any() 
            ? Ok("No expense categories for this account") 
            : Ok(categories.Select(Mapper.CreateExpenseCategoryViewModel).ToList());
    }
}