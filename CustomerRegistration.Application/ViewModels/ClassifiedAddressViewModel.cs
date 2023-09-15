using System.Text.Json.Serialization;
using CustomerRegistration.Domain.Models.Entities;

namespace CustomerRegistration.Application.ViewModels;

public record ClassifiedAddressViewModel : BaseEntityViewModel
{
    public ClassifiedAddressViewModel(
        Guid id, DateTime createAt, DateTime? lastUpdate,
        bool isMain, string classified, string postalCode, string state, string city, string neighborhood, string street, uint number, string complement)
        : base(id, createAt, lastUpdate)
    {
        IsMain = isMain;
        Classified = classified;
        PostalCode = postalCode;
        State = state;
        City = city;
        Neighborhood = neighborhood;
        Street = street;
        Number = number;
        Complement = complement;
    }

    [JsonPropertyName("is_main")]
    public bool IsMain { get; private set; }

    [JsonPropertyName("classified")]
    public string Classified { get; private set; } = string.Empty;

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; private set; } = string.Empty;

    [JsonPropertyName("state")]
    public string State { get; private set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; private set; } = string.Empty;

    [JsonPropertyName("neighborhood")]
    public string Neighborhood { get; private set; } = string.Empty; 

    [JsonPropertyName("street")]
    public string Street { get; private set; } = string.Empty;

    [JsonPropertyName("number")]
    public uint Number { get; private set; }

    [JsonPropertyName("complement")]
    public string Complement { get; private set; } = string.Empty;

    public static ClassifiedAddressViewModel MapFromDomain(ClassifiedAddress entity) => 
        new ClassifiedAddressViewModel(
            entity.Id,
            entity.CreateAt,
            entity.LastUpdate,
            entity.IsMain,
            entity.Classified,
            entity.Address.PostalCode,
            entity.Address.State,
            entity.Address.City,
            entity.Address.Neighborhood,
            entity.Address.Street,
            entity.Address.Number,
            entity.Address.Complement
        );

    public static IEnumerable<ClassifiedAddressViewModel> MapFromDomain(IList<ClassifiedAddress> entity)
    {
        foreach (var item in entity)
        {
            yield return MapFromDomain(item);
        }
    }
}
