using System.Text.Json.Serialization;

namespace CustomerRegistration.Application.ViewModels;

public record BaseEntityViewModel
{
    public BaseEntityViewModel(Guid id, DateTime createAt, DateTime? lastUpdate)
    {
        Id = id;
        CreateAt = createAt;
        LastUpdate = lastUpdate;
    }

    [JsonPropertyName("id")]
    public Guid Id { get; private set; }

    [JsonPropertyName("create_at")]
    public DateTime CreateAt { get; private set; }

    [JsonPropertyName("last_update")]
    public DateTime? LastUpdate { get; private set; }
}
