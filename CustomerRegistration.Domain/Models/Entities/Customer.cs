
using CustomerRegistration.Domain.Models.Abstracts;
using CustomerRegistration.Domain.Models.ValueObject;

namespace CustomerRegistration.Domain.Models.Entities;

public class Customer : BaseEntity
{
    private Customer() {}
    public Customer(
        Guid id,
        Name name, 
        CellPhone cellPhone, 
        Email mainEmail
        ) : base(id)
    {
        Name = name;
        CellPhone = cellPhone;
        MainEmail = mainEmail;
        ClassifiedAdresses = new List<ClassifiedAddress>();
        ApplyValidation();
    }

    public Name Name { get; private set; }
    public CellPhone CellPhone { get; private set; }

    public Email MainEmail { get; private set; }
    public Email? RecoveryEmail { get; private set; }
    public IList<ClassifiedAddress> ClassifiedAdresses { get; set; }

    public void AddAddress(ClassifiedAddress address)
    {
        if (ClassifiedAdresses == null)
        {
            ClassifiedAdresses = new List<ClassifiedAddress>();
        }

        var classifiedAdressesCount = ClassifiedAdresses.Count;
        if (classifiedAdressesCount == 0 && !address.IsMain)
        {
            AddError("The customer must have at least one main address.");
            ThrowDomainError();
        }
        if (ClassifiedAdresses.Count > 0)
        {
            var isMainCount = ClassifiedAdresses.Where(x => x.IsMain).Count();
            if (isMainCount == 1 && address.IsMain)
            {
                AddError("The customer must have only one main address.");
                ThrowDomainError();
            }
        }
        ClassifiedAdresses.Add(address);
        LastUpdate = DateTime.Now;
    }

    public void AddRecoveryEmail(Email email)
    {
        if (MainEmail.Text != email.Text)
        {
            RecoveryEmail = email;
            LastUpdate = DateTime.Now;
        }
        else {
            AddError("The recovery email must be different from the main email.");
            ThrowDomainError();
        }
    }

    protected override void ApplyValidation()
    {
        if (ClassifiedAdresses == null)
        {
            AddError("The classifiedAddresses cannot be null.");
        }
    }
}
