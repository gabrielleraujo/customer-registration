using CustomerRegistration.Domain.Models.Entities;
using CustomerRegistration.Domain.Models.ValueObject;

namespace CustomerRegistration.Test.Factories.Domain
{
    public static class CustomerFactory
    {
        public static Customer Get() => new Customer(
            id: Guid.NewGuid(),
            new Name("Gabrielle", "Souza"),
            new CellPhone("21912345678"),
            new Email("gabrielleraujo@gmail.com")
        );
    }
}