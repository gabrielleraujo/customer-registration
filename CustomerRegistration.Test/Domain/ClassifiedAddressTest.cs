using CustomerRegistration.Domain.Models.Exceptions;
using CustomerRegistration.Test.Factories.Domain;
using FluentAssertions;

namespace CustomerRegistration.Test.Domain
{
    public class ClassifiedAddressTest
    {
        [Fact]
        public void ClassifiedAddressWhenClassifiedIsEmptyThenNeedNotThowsDomainException()
        {
            Action act = () => ClassifiedAddressFactory.Get(Guid.NewGuid(), classified: "");
            act.Should().Throw<DomainException>().WithMessage("The Classified cannot be null or empty.");
        }

        [Fact]
        public void ClassifiedAddressWhenClassifiedIsNotNullAndNotEmptyThenNeedNotThowsDomainException()
        {
            Action act = () => ClassifiedAddressFactory.Get(Guid.NewGuid());
            act.Should().NotThrow<DomainException>();
        }
    }
}