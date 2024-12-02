using BancoChuSA.Domain.Entities;

namespace BancoChuSA.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> Get();
}
