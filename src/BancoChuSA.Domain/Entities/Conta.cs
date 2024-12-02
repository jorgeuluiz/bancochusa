namespace BancoChuSA.Domain.Entities;

public class Conta
{
    public long Id { get; set; }
    public string NumeroConta { get; set; } = string.Empty;
    public decimal Saldo { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;

}
