using CoreX.Base;
using Doit.AccountModule.Presentation.Consumer;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Support.AuthModule.Messaging;

namespace Doit.AccountModule.Consumer
{
    public static class ServiceConfigurator
    {
        public static void AddConsumers(this IBusRegistrationConfigurator busConfigurator)
        {
            busConfigurator.AddConsumer<GlobalFaultConsumer>();
        }
        public static void ConfigureConsumers(
            this IBusFactoryConfigurator busConfigurator,
            IBusRegistrationContext context)
        {
            busConfigurator.ReceiveEndpoint(
                AuthModuleEndpointQueues.Default,
                endpoint =>
            {
                if (new AppEnvironment().State != EnvironmentState.Development)
                    endpoint.UseMessageRetry(r => r.Interval(3, TimeSpan.FromMinutes(15)));
                //e.UseInMemoryOutbox();
                endpoint.ConcurrentMessageLimit = 100;
                endpoint.PrefetchCount = 16;

    
                endpoint.ConfigureConsumer<GlobalFaultConsumer>(context);
            });
        }
    }
}
