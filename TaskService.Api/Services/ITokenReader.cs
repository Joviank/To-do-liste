using TaskService.Api.Models;
namespace TaskService.Api.Services;

public interface ITokenReader
{
    TokenInfo? ReadToken(string token);

    Guid? GetUserId(string authHeader);
    string CreateToken(User user, string key);
}