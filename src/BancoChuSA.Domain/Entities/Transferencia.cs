namespace BancoChuSA.Domain.Entities;

public class Transferencia
{
    public long Id { get; set; }   
    public decimal Valor { get; set; } 
    public DateTime Data { get; set; }        
    public string NumeroConta { get; set; } = string.Empty;
}
