using BancoChuSA.Domain.Entities;
using BancoChuSA.Domain.Security.Tokens;
using BancoChuSA.Domain.Services.LoggedUser;
using BancoChuSA.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BancoChuSA.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly BancoChuSADbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(BancoChuSADbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await
            _dbContext.Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}
