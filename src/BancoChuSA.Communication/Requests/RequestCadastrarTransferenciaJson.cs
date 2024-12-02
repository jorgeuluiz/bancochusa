namespace BancoChuSA.Communication.Requests;

public class RequestCadastrarTransferenciaJson
{
    public decimal Valor { get; set; }

    public string NumeroConta { get; set; } = string.Empty;

    public DateTime Data { get; set; }

}
