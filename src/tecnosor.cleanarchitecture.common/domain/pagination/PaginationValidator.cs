namespace tecnosor.cleanarchitecture.common.domain.pagination;

public class PaginationValidator : ValueObject, IPaginationValidator
{
    private readonly int _minPageSize = 1;
    private readonly int _maxPageSize = 1_000;
    private readonly int _minPageNumber = 1;
    private readonly int _maxPageNumber = 100_000_000;

    public void Validate(long pageNumber, int pageSize)
    {
        ValidatePageSize(pageSize);
        ValidatePageNumber(pageNumber);
    }

    private void ValidatePageSize(int pageSize)
    {
        if (pageSize < _minPageSize || pageSize > _maxPageSize)
            throw new PaginationException("PageSize cannot be negative or greater than 1000.");
    }

    private void ValidatePageNumber(long pageNumber)
    {
        if (pageNumber < _minPageNumber || pageNumber > _maxPageNumber)
            throw new PaginationException("PageNumber cannot be negative or greater than 100,000,000.");
    }
}
