using TaskService.Api.Models;
using System.IdentityModel.Tokens.Jwt;

namespace TaskService.Api.Services;

public class TokenReader : ITokenReader
{
    public TokenInfo? ReadToken(string authHeader)    
    {
        // Sjekker at det som ble sendt inn ikke var tomt eller om den ikke starter med Bearer 
        if(string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith(
            "Bearer ",
            StringComparison.OrdinalIgnoreCase
        ))
        {
            return null;
        }

        // Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.xxxxxxx.xxxxxxx
        var token = authHeader.Replace("Bearer ", "");

        // eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.xxxxxxx.xxxxxxx
        var handler = new JwtSecurityTokenHandler();
        if (!handler.CanReadToken(token))
        {
            return null;
        }
        var jwt = handler.ReadJwtToken(token);

        /* 
            { HEADER
            "alg": "HS256",
            "typ": "JWT"
            }

            { CLAIMS
            "sub": "dsd89-sd9s0-dsfjs",
            "name": "Hode Kode",
            "admin": true,
            "iat": 1792571298
            } 
        */

        return new TokenInfo
        {
            Header = jwt.Header.ToDictionary(
                item => item.Key,
                item => item.Value),
            Claims = jwt.Claims.Select( claim => new TokenInfo.TokenClaim
            {
                Type = claim.Type,
                Value = claim.Value
            }).ToList()
        };
    }

    public Guid? GetUserId(string authHeader)
    {
        var tokenInfo = ReadToken(authHeader);
        if (tokenInfo == null)
        {
            return null;
        }
        var userIdClaim = tokenInfo.Claims.FirstOrDefault(claim => claim.Type == "sub");

        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return null;
        }
        return userId;
    }
}