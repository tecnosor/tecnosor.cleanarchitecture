namespace tecnosor.cleanarchitecture.common.domain;
[Serializable]
public class ExistException : DomainException
{
    public ExistException() : base() { }
    public ExistException(string message) : base(message) { }
    public ExistException(string message, Exception ex) : base(message, ex) { }
}
