namespace tecnosor.cleanarchitecture.common.domain;

public class Id : ValueObject
{
    private string _id;
    public string Value 
    { 
        get => _id;
        set => setId(value);
    }

    public Id(string id) => setId(id);
    private void setId(string id)
    {
        if (id == " " || Guid.TryParse(id, out _)) _id = id;
        else throw new ValidationException("Invalid GUID format");
    }
}
