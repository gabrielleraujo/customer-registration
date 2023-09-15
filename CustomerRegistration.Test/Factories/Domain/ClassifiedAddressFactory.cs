using CustomerRegistration.Domain.Models.Entities;

namespace CustomerRegistration.Test.Factories.Domain
{
    public static class ClassifiedAddressFactory
    {
        public static ClassifiedAddress Get(Guid customerId, string classified = "home", bool isMain = true) => new ClassifiedAddress(
            id: Guid.NewGuid(),
            customerId: customerId,
            isMain: isMain,
            classified: classified,
            AddressFactory.Get()
        );
    }
}