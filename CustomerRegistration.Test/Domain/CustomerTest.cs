using CustomerRegistration.Domain.Models.Exceptions;
using CustomerRegistration.Test.Factories.Domain;
using FluentAssertions;

namespace CustomerRegistration.Test.Domain
{
    public class CustomerTest
    {
        [Fact]
        public void AddAddressWhenFirstInsertIsNotMainAddressThenNeedThowsDomainException()
        {
            var customer = CustomerFactory.Get();
            var address = AddressFactory.Get();

            Action act = () => customer.AddAddress(ClassifiedAddressFactory.Get(
                customer.Id,
                classified: "home",
                isMain: false
            ));

            act.Should().Throw<DomainException>().WithMessage("The customer must have at least one main address.");
        }

        [Fact]
        public void AddAddressWhenTyInsertMoreThan1IsMainAddressThenNeedThowsDomainException()
        {
            var customer = CustomerFactory.Get();
            var address = AddressFactory.Get();

            customer.AddAddress(ClassifiedAddressFactory.Get(
                customer.Id,
                classified: "home",
                isMain: true
            ));

            Action act = () => customer.AddAddress(ClassifiedAddressFactory.Get(
                customer.Id,
                classified: "work",
                isMain: true
            ));

            act.Should().Throw<DomainException>().WithMessage("The customer must have only one main address.");
        }

        [Fact]
        public void AddAddressWhenInsertOneIsMainAddressThenNeedNotThowsDomainException(string email)
        {
            var customer = CustomerFactory.Get();
            var address = AddressFactory.Get();

            Action act = () => customer.AddAddress(ClassifiedAddressFactory.Get(
                customer.Id,
                classified: "work",
                isMain: true
            ));

            act.Should().NotThrow<DomainException>();
        }
    }
}