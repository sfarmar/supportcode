namespace Ordering.Server
{
    using NServiceBus;
    using NServiceBus.Features;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.Features.Disable<Sagas>();
            Configure.Features.Disable<TimeoutManager>();
        }
    }
}