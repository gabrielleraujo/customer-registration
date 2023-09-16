using CustomerRegistration.Domain.Models.Exceptions;
using CustomerRegistration.Domain.Models.ValueObject;
using FluentAssertions;

namespace CustomerRegistration.Test.Domain
{
    public class NameTest
    {
        [Fact]
        public void NameWhenFirstNameIsEmptyThenNeedNotThowsDomainException()
        {
            Action act = () => new Name("", "Souza");
            act.Should().Throw<DomainException>().WithMessage("\nErrors:\nFirstName could not be null ou empty.");
        }

        [Fact]
        public void NameWhenLastNameIsEmptyThenNeedNotThowsDomainException()
        {
            Action act = () => new Name("Gabrielle", "");
            act.Should().Throw<DomainException>().WithMessage("\nErrors:\nLastName could not be null ou empty.");
        }

        [Fact]
        public void NameWhenFirstAndLastNameIsEmptyThenNeedNotThowsDomainException()
        {
            Action act = () => new Name("", "");
            act.Should().Throw<DomainException>().WithMessage("\nErrors:\nFirstName could not be null ou empty.\nLastName could not be null ou empty.");
        }

        [Fact]
        public void NameWhenFirstAndLastNameIsNotEmptyThenNeedNotThowsDomainException()
        {
            Action act = () => new Name("Gabrielle", "Souza");
            act.Should().NotThrow<DomainException>();
        }
    }
}