using BancoChuSA.Domain.Entities;

namespace BancoChuSA.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{

    string Generate(User user);
}
