using System.Net;

namespace BancoChuSA.Exception.ExceptionsBase;

public class InvalidLoginException : BancoChuSAException
{
    public InvalidLoginException() : base(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return new List<string> { Message };
    }
}
