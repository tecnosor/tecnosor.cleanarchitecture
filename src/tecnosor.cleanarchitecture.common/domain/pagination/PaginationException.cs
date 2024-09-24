namespace tecnosor.cleanarchitecture.common.domain.pagination;

public class PaginationException : ValidationException 
{
    public PaginationException() : base() { }
    public PaginationException(string message) : base(message) { }
    public PaginationException(string message, Exception ex) : base(message, ex) { }
}
