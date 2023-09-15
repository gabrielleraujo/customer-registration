using CustomerRegistration.Application.ViewModels;

namespace CustomerRegistration.Test.Factories.Application
{
    public static class ClassifiedAddressViewModelFactory
    {
        public static ClassifiedAddressViewModel Get(bool isMain = true) => new ClassifiedAddressViewModel(
            id: Guid.NewGuid(),
            createAt: DateTime.Now,
            lastUpdate: DateTime.Now.AddDays(1),
            isMain: isMain,
            classified: "home",
            postalCode: "12345678",
            state: "Rio de Janeiro",
            city: "Rio de Janeiro",
            neighborhood: "Vila de Cava",
            street: "Rua A",
            number: 100,
            complement: "house"
        );
    }
}