using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using WalletAPI.ViewModels.IncomeStatementViewModels;

namespace WalletAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class IncomeStatementController : ControllerBase
{
    private readonly IIncomeStatementService _incomeStatementService;

    public IncomeStatementController(IIncomeStatementService incomeStatementService)
    {
        _incomeStatementService = incomeStatementService;
    }

    //[EnableCors("WalletCorsPolicy")]
    [HttpPost("/IncomeStatement")]
    public IActionResult CreateIncomeStatement([FromForm] CreateIncomeStatementViewModel model)
    {
        if (string.IsNullOrEmpty(model.UserName) 
            || string.IsNullOrEmpty(model.AccountName)
            || string.IsNullOrEmpty(model.CategoryName))
            return BadRequest("Empty fields");

        if (model.Amount <= decimal.Zero)
            return BadRequest("Wrong amount of the statement");
        
        try
        {
            _incomeStatementService.CreateIncomeStatement(model.UserName, model.AccountName, model.CategoryName, model.Amount);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }

        //return Ok(model);
        return StatusCode(StatusCodes.Status201Created, model);
    }
}