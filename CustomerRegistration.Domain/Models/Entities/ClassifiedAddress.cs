using CustomerRegistration.Domain.Models.Abstracts;
using CustomerRegistration.Domain.Models.ValueObject;

namespace CustomerRegistration.Domain.Models.Entities;

public class ClassifiedAddress : BaseEntity
{
    private ClassifiedAddress() {}
    public ClassifiedAddress(
        Guid id,
        Guid customerId,
        bool isMain, 
        string classified, 
        Address address) : base(id)
    {
        CustomerId = customerId;
        IsMain = isMain;
        Classified = classified;
        Address = address;
        Validate();
    }

    public Guid CustomerId { get; private set; }
    public bool IsMain { get; private set; }
    public string Classified { get; private set; } = string.Empty;
    public Address Address { get; private set; }

    protected override void ApplyValidation()
    {
        if (string.IsNullOrEmpty(Classified)) AddError("The Classified cannot be null or empty.");
    }
}
