using CustomerRegistration.Domain.Models.ValueObject;

namespace CustomerRegistration.Test.Factories.Domain
{
    public static class AddressFactory
    {
        public static Address Get() => new Address(
            postalCode: "12345678",
            state: "Rio de Janeiro",
            city: "Rio de Janeiro",
            neighborhood: "Vila de Cava",
            street: "Rua A",
            number: 100,
            complement: "house"
        );
    }
}