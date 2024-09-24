namespace tecnosor.cleanarchitecture.common.domain;

public class Aggregate
{
    protected string _id;
    public AuditFields Audit { get; set; }
    public string Id
    {
        get => _id;
        set => _id = new Id(value).Value;
    }
    // TODO: Audit fields
}
