using CustomerRegistration.Application.ViewModels;

namespace CustomerRegistration.Test.Factories.Application
{
    public static class CustomerViewModelFactory
    {
        public static CustomerViewModel Get => new CustomerViewModel(
            id: Guid.NewGuid(),
            createAt: DateTime.Now,
            lastUpdate: DateTime.Now.AddDays(1),
            firstName: "Gabrielle",
            lastName: "Souza",
            cellPhone: "21912345678",
            mainEmail: "gabrielleraujo@gmail.com",
            recoveryEmail: "teste@gmail.com",
            classifiedAdresses: new List<ClassifiedAddressViewModel>() {
                ClassifiedAddressViewModelFactory.Get()
            }
        );
    }
}