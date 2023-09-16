using CustomerRegistration.Domain.Models.Exceptions;
using CustomerRegistration.Domain.Models.ValueObject;
using CustomerRegistration.Test.Factories.Domain;
using FluentAssertions;

namespace CustomerRegistration.Test.Domain
{
    public class AddressTest
    {
        [Fact]
        public void AddressWhenStateIsEmptyThenNeedThowsDomainException()
        {
            Action act = () => new Address(
                postalCode: "12345678",
                state: "",
                city: "Rio de Janeiro",
                neighborhood: "Vila de Cava",
                street: "Rua A",
                number: 100,
                complement: "house"
            );
            act.Should().Throw<DomainException>().WithMessage("\nErrors:\nThe State cannot be null or empty.");
        }

        [Fact]
        public void AddressWhenAllValuesIsInformedThenNeedNotThowsDomainException()
        {
            Action act = () => AddressFactory.Get();
            act.Should().NotThrow<DomainException>();
        }
    }
}
