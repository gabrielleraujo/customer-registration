namespace CustomerRegistration.Domain.Models.Abstracts;

public abstract class BaseEntity : BaseDomainModel
{
    public Guid Id { get; private set; }
    public DateTime CreateAt { get; private set; }
    public DateTime? LastUpdate { get; protected set; }

    protected BaseEntity() {}

    public BaseEntity(Guid id)
    {
        Id = id;
        CreateAt = DateTime.Now;
        LastUpdate = null;
    }
}
