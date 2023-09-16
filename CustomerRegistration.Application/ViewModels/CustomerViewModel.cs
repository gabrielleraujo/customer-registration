using System.Text.Json.Serialization;
using CustomerRegistration.Domain.Models.Entities;

namespace CustomerRegistration.Application.ViewModels;

public record CustomerViewModel : BaseEntityViewModel
{
    public CustomerViewModel(
        Guid id, DateTime createAt, DateTime? lastUpdate,
        string firstName, string lastName, string cellPhone, string mainEmail, string? recoveryEmail, IList<ClassifiedAddressViewModel>? classifiedAdresses)
        : base(id, createAt, lastUpdate)
    {
        FirstName = firstName;
        LastName = lastName;
        CellPhone = cellPhone;
        MainEmail = mainEmail;
        RecoveryEmail = recoveryEmail;
        ClassifiedAdresses = classifiedAdresses;
    }

    [JsonPropertyName("first_name")]
    public string FirstName { get; private set; } = string.Empty;

    [JsonPropertyName("last_name")]
    public string LastName { get; private set; } = string.Empty;

    [JsonPropertyName("cell_phone")]
    public string CellPhone { get; private set; } = string.Empty;

    [JsonPropertyName("main_email")]
    public string MainEmail { get; private set; } = string.Empty;

    [JsonPropertyName("recovery_email")]
    public string? RecoveryEmail { get; private set; } = null;

    [JsonPropertyName("classified_address")]
    public IList<ClassifiedAddressViewModel>? ClassifiedAdresses { get; private set; } = null;

    public static CustomerViewModel MapFromDomain(Customer entity)
    {
        IList<ClassifiedAddressViewModel> classifiedAdresses = new List<ClassifiedAddressViewModel>();

        foreach(var item in entity.ClassifiedAdresses)
        {
            classifiedAdresses.Add(ClassifiedAddressViewModel.MapFromDomain(item!));
        }
        
        var customerViewModel = new CustomerViewModel(
            entity.Id,
            entity.CreateAt,
            entity.LastUpdate,
            entity.Name.First,
            entity.Name.Last,
            entity.CellPhone.Number,
            entity.MainEmail.Text,
            entity.RecoveryEmail == null ? null : entity.RecoveryEmail.Text,
            classifiedAdresses
        );
        return customerViewModel;
    }

    public static IEnumerable<CustomerViewModel> MapFromDomain(IList<Customer> entity)
    {
        foreach (var item in entity)
        {
            yield return MapFromDomain(item);
        }
    }
}
