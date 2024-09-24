namespace tecnosor.cleanarchitecture.common.domain;

public class Entity
{
    protected string _id;
    public AuditFields Audit { get; }
    public string Id
    {
        get => _id;
        set => _id = new Id(value).Value;
    }
    public Entity(string id, AuditFields audit)
    {
        Id = id;
        Audit = audit;
    }

    public Entity(string id) : this(id, new AuditFields(DateTime.Now, DateTime.Now, " ", " ")) { }
}
