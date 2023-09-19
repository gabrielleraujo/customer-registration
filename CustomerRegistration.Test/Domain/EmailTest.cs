using CustomerRegistration.Domain.Models.Exceptions;
using CustomerRegistration.Domain.Models.ValueObject;
using FluentAssertions;

namespace CustomerRegistration.Test.Domain
{
    public class EmailTest
    {
        [Theory]
        [InlineData("gabrielleraujogmail.com")]
        [InlineData("teste@gmal")]
        public void EmailWhenEmailIsNotMatchPatternThenNeedThowsDomainException(string email)
        {
            Action act = () => new Email(email);
            act.Should().Throw<DomainException>().WithMessage("\nErrors:\nThe e-mail is invalid.");
        }

        [Theory]
        [InlineData("gabrielleraujo@gmail.com")]
        [InlineData("teste@hotmail.com")]
        public void EmailWhenEmailIsMatchPatternThenNeedNotThowsDomainException(string email)
        {
            Action act = () => new Email(email);
            act.Should().NotThrow<DomainException>();
        }
    }
}
