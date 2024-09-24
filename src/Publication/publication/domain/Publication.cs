using tecnosor.cleanarchitecture.common.domain;

namespace stolenCars.publication.domain;

public class Publication : Aggregate
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Vehicle Vehicle { get; set; }
    public decimal Reward { get; set; }
    public PublicationStatus Status { get; set; }
    public IdentityUser IdentityUser { get; set; }

    public Publication(string id,
                       string name, 
                       string description, 
                       Vehicle vehicle, 
                       decimal reward, 
                       PublicationStatus status, 
                       IdentityUser identityUser)
    {
        Id = id;
        Name = name;
        Description = description;
        Vehicle = vehicle;
        Reward = reward;
        Status = status;
        IdentityUser = identityUser;
    }
}

public enum PublicationStatus
{
    DRAFT,
    IN_FINDING,
    RESOLVED
}