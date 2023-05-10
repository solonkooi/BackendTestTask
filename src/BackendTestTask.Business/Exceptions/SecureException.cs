namespace BackendTestTask.Business.Exceptions;

public class SecureException : Exception
{
    public SecureException(string? message = null) : base(message ?? "Secure exception")
    {
    }
}