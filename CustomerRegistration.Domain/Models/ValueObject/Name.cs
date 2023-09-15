using CustomerRegistration.Domain.Models.Abstracts;

namespace CustomerRegistration.Domain.Models.ValueObject;

public class Name : BaseDomainModel
{
    public Name(
        string first, 
        string last)
    {
        First = first;
        Last = last;
        Validate();
    }

    public string First { get; private set; }
    public string Last { get; private set; }

    protected override void ApplyValidation()
    {
        if (string.IsNullOrEmpty(First))
        {
            AddError("FirstName could not be null ou empty.");
        }
        if (string.IsNullOrEmpty(Last))
        {
            AddError("LastName could not be null ou empty.");
        }
    }
}
