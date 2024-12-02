using System.Net;

namespace BancoChuSA.Exception.ExceptionsBase;

public class NotFoundException : BancoChuSAException
{
    public NotFoundException(string message) : base(message)
    {
        
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return new List<string>() { Message };
    }
}

