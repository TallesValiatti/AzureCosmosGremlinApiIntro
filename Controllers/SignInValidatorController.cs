using AzureCosmosGremlinApiIntro.Models;
using AzureCosmosGremlinApiIntro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzureCosmosGremlinApiIntro.Controllers;

[ApiController]
[Route("[controller]")]
public class SignInValidatorController : ControllerBase
{
    private readonly ISignInValidatorService _loginValidatorService;

    public SignInValidatorController(ISignInValidatorService loginValidatorService)
    {
        _loginValidatorService = loginValidatorService;
    }

    [HttpPost(Name = "ValidateSignInData")]
    public async Task<IActionResult> Post([FromBody]SignInData data)
    {
        return Ok(new 
        {
            IsValid = await _loginValidatorService.IsValidAsync(data)
        });
    }
}
