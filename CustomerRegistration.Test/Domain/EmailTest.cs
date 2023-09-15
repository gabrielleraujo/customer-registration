using CustomerRegistration.Domain.Models.Exceptions;
using CustomerRegistration.Domain.Models.ValueObject;
using FluentAssertions;

namespace CustomerRegistration.Test.Domain
{
    public class EmailTest
    {
        [Theory]
        [InlineData("gabrielleraujo@gmail.com")]
        [InlineData("teste@hotmail.com")]
        public void EmailWhenMailAddressReturnFalseThenNeedNotThowsDomainException(string email)
        {
            Action act = () => new Email(email);
            act.Should().Throw<DomainException>().WithMessage("The e-mail is invalid.");
        }

        [Theory]
        [InlineData("gabrielleraujogmail.com")]
        [InlineData("teste@gmail")]
        public void EmailWhenMailAddressIsCreateWithSuccessThenNeedNotThowsDomainException(string email)
        {
            Action act = () => new CellPhone(email);
            act.Should().NotThrow<DomainException>();
        }
    }
}
