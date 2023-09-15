using CustomerRegistration.Domain.Models.Abstracts;

namespace CustomerRegistration.Domain.Models.ValueObject;

public class Address : BaseDomainModel
{
    public Address(
        string postalCode, 
        string state, 
        string city, 
        string neighborhood, 
        string street, 
        uint number, 
        string complement)
    {
        PostalCode = postalCode;
        State = state;
        City = city;
        Neighborhood = neighborhood;
        Street = street;
        Number = number;
        Complement = complement;
        Validate();
    }

    public string PostalCode { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Neighborhood { get; private set; } = string.Empty; 
    public string Street { get; private set; } = string.Empty;
    public uint Number { get; private set; }
    public string Complement { get; private set; } = string.Empty;

    protected override void ApplyValidation()
    {
        if (string.IsNullOrEmpty(PostalCode)) AddError("The PostalCode cannot be null or empty.");
        if (string.IsNullOrEmpty(State)) AddError("The State cannot be null or empty.");
        if (string.IsNullOrEmpty(City)) AddError("The City cannot be null or empty.");
        if (string.IsNullOrEmpty(Neighborhood)) AddError("The Neighborhood cannot be null or empty.");
        if (string.IsNullOrEmpty(Street)) AddError("The Street cannot be null or empty.");
        if (string.IsNullOrEmpty(Complement)) AddError("The Complement cannot be null or empty.");
    }
}
