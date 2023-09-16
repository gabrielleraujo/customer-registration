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

            Action act = () => customer.AddAddress(ClassifiedAddressFactory.Get(
                customer.Id,
                classified: "home",
                isMain: false
            ));

            act.Should().Throw<DomainException>().WithMessage("\nErrors:\nThe customer must have at least one main address.");
        }

        [Fact]
        public void AddAddressWhenTyInsertMoreThan1IsMainAddressThenNeedThowsDomainException()
        {
            var customer = CustomerFactory.Get();

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

            act.Should().Throw<DomainException>().WithMessage("\nErrors:\nThe customer must have only one main address.");
        }

        [Fact]
        public void AddAddressWhenInsertOneIsMainAddressThenNeedNotThowsDomainException()
        {
            var customer = CustomerFactory.Get();

            Action act = () => customer.AddAddress(ClassifiedAddressFactory.Get(
                customer.Id,
                classified: "work",
                isMain: true
            ));

            act.Should().NotThrow<DomainException>();
        }
    }
}