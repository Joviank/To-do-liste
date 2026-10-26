using Microsoft.AspNetCore.Mvc;
using TaskService.Api.Data;
using TaskService.Api.DTO;
using TaskService.Api.Services;
using TaskService.Api.Security;

namespace TaskService.Api.Controllers;

[ApiController]
[Route("Auth")]
public class AuthController : ControllerBase
{
    private readonly ITokenReader _tokenReader;
    private readonly TaskDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthController(ITokenReader tokenReader, TaskDbContext context, IConfiguration configuration)
    {
        _tokenReader = tokenReader;
        _context = context;
        _configuration = configuration;
    }
    [HttpPost("Register")]
    public IActionResult Register(RegisterRequest request)
    {
        if(_context.Users.Any(u => u.Username == request.Username))
        {
            return BadRequest("This username already exists.");    
        }
        _context.Users.Add(new User
        {
            Username = request.Username,
            Password = HashPasswords.HashAndSaltPassword(request.Password)
        });
        _context.SaveChanges();
        return Ok("User has been registered successfully.");
    }
    [HttpPost("Login")]
    public IActionResult Login(LoginRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
        if (user == null || !HashPasswords.VerifyPassword(request.Password, user.Password))
        {
            return Unauthorized("Invalid username or password");
        }
        var token = _tokenReader.CreateToken(user, _configuration["Jwt:Key"]!);
        return Ok(new {token});
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