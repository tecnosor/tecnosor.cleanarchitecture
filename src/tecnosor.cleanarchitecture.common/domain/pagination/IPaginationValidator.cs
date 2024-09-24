namespace tecnosor.cleanarchitecture.common.domain.pagination;

public interface IPaginationValidator
{
    void Validate(long pageNumber, int pageSize);
}
