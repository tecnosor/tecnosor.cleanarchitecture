namespace tecnosor.cleanarchitecture.common.domain;
[Serializable]
public class ValidationException : DomainException
{
    public ValidationException() : base() { }
    public ValidationException(string message) : base(message) { }
    public ValidationException(string message, Exception ex) : base(message, ex) { }
}
