using CustomerRegistration.Domain.Models.Exceptions;
using CustomerRegistration.Domain.Models.ValueObject;
using FluentAssertions;

namespace CustomerRegistration.Test.Domain
{
    public class CellPhoneTest
    {
        [Theory]
        [InlineData("(03) 99010-0070")]
        [InlineData("(33) 29900-8000")]
        [InlineData("(00) 00000-0000")]
        [InlineData("(21) 9900-0002")]
        [InlineData("21 79900-8070")]
        [InlineData("(88) 00000-0000")]
        public void CellPhoneWhenNumberIsNotMatchPatternThenNeedThowsDomainException(string cellphone)
        {
            Action act = () => new CellPhone(cellphone);
            act.Should().Throw<DomainException>().WithMessage("\nErrors:\nThe cell phone number is invalid.");
        }

        [Theory]
        [InlineData("(88) 97845-8070")]
        [InlineData("(12) 97925-8552")]
        [InlineData("21971573847")]
        [InlineData("8297845-8070")]
        [InlineData("12 7157-3847")]
        public void CellPhoneWhenNumberIsMatchPatternThenNeedNotThowsDomainException(string cellphone)
        {
            Action act = () => new CellPhone(cellphone);
            act.Should().NotThrow<DomainException>();
        }
    }
}
