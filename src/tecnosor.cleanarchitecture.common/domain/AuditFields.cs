namespace tecnosor.cleanarchitecture.common.domain;

public class AuditFields : ValueObject
{
    public DateTime CreatedDate { get; }
    public DateTime LastModifiedDate { get; }
    public string LastModifiedBy { get; }
    public string CreatedBy { get; }
    public AuditFields(DateTime createdDate, DateTime lastModifiedDate, string lastModifiedBy, string createdBy)
    {
        if (createdDate > lastModifiedDate) throw new ValidationException("Created Date cannot be lower than Last modified Date");
        ValidateUserGuid(createdBy, "createdBy");
        ValidateUserGuid(createdBy, "createdBy");
        CreatedDate = createdDate;
        LastModifiedDate = lastModifiedDate;
        LastModifiedBy = lastModifiedBy;
        CreatedBy = createdBy;
    }

    private void ValidateUserGuid(string guid, string fieldName)
    {
        try
        {
            new Id(guid);
        } catch(ValidationException) 
        {
            throw new ValidationException($"AuditFields.{fieldName} Guid format is not valid.");
        }
    }
}
