using Microsoft.AspNetCore.Mvc;
using TaskService.Api.Services;

namespace TaskService.Api.Controllers;

[ApiController]
[Route("Auth")]
public class AuthController : ControllerBase
{
    private readonly ITokenReader _tokenReader;
    public AuthController(ITokenReader tokenReader)
    {
        _tokenReader = tokenReader;
    }

    [HttpGet("Token")]
    public IActionResult ReadToken()
    {
        var result = _tokenReader.ReadToken(Request.Headers.Authorization.ToString());

        if (result is null)
        {
            return BadRequest("Invalid or unreadable token");
        }
        return Ok(result);
    }
}