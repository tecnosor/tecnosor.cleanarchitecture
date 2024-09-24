namespace tecnosor.cleanarchitecture.common.domain.match;

public class RestFilter(string field, string operation, string value)
{
    public string Field { get; } = field;
    public string Operation { get; } = operation;
    public string Value { get; } = value;
}