namespace BancoChuSA.Exception.ExceptionsBase;

public abstract class BancoChuSAException : SystemException
{
    protected BancoChuSAException(string message) : base(message)
    {
            
    }

    public abstract int StatusCode { get; }

    public abstract List<string> GetErrors();
}
