namespace BancoChuSA.Communication.Responses;

public class ResponseTransferenciaJson
{
    public List<ResponseShortTransferenciaJson> Transferencias { get; set; } = new List<ResponseShortTransferenciaJson>();
}
