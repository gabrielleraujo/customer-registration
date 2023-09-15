using System.Net.Mail;
using CustomerRegistration.Domain.Models.Abstracts;

namespace CustomerRegistration.Domain.Models.ValueObject;

public class Email : BaseDomainModel
{
    public Email(string text)
    {
        Text = text;
        Validate();
    }

    public string Text { get; private set; } = string.Empty;

    protected override void ApplyValidation()
    {
        var isSuccess = MailAddress.TryCreate(Text, out _);
        if (!isSuccess)
        {
            AddError("The e-mail is invalid.");
        }
    }
}
