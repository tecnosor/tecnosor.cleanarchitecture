namespace tecnosor.cleanarchitecture.common.domain.pagination;

public class Pagination
{
    public int PageNumber { get; } = 1;
    public int PageSize { get; } = 10;

    private readonly IPaginationValidator _validator = new PaginationValidator();

    public Pagination(int pageNumber, int pageSize)
    {
        // Validar los parámetros de entrada
        _validator.Validate(pageNumber, pageSize);

        // Asignar valores si son válidos
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}