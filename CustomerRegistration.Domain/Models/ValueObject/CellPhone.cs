using System.Text.RegularExpressions;
using CustomerRegistration.Domain.Models.Abstracts;

namespace CustomerRegistration.Domain.Models.ValueObject;

public class CellPhone : BaseDomainModel
{    
    public CellPhone(string number)
    {
        Number = number;
        Validate();
    }

    public string Number { get; set; }

    protected override void ApplyValidation()
    {
        var result = Regex.Match(Number, "^\\(?(?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\\)? ?(?:[2-8]|9[1-9])[0-9]{3}\\-?[0-9]{4}$");

        if (!result.Success)
        {
            AddError("The cell phone number is invalid.");
        }
    }
}
