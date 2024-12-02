namespace BancoChuSA.Communication.Responses;

public class ResponseShortTransferenciaJson
{    
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public string NumeroConta { get; set; } = string.Empty;
}
