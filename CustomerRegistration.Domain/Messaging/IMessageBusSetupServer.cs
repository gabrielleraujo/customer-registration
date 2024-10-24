namespace CustomerRegistration.Domain.Messaging
{
    public interface IMessageBusSetupServer
    {
        void Setup();
        void Dispose();
    }
}
