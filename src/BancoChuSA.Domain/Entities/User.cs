using BancoChuSA.Domain.Enums;

namespace BancoChuSA.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public Guid UserIdentifier { get; set; }

    public string Role { get; set; } = Roles.TEAM_MEMBER;

}
