namespace tecnosor.cleanarchitecture.common.domain;
[Serializable]
public class NotFoundException : DomainException
{
    public NotFoundException() : base() { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception ex) : base(message, ex) { }
}