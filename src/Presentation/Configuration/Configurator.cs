using Doit.AccountModule.Application;
using MassTransit.CoreX;
using MassTransit;
using CoreX.Settings;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Doit.AccountModule.Presentation.Configuration
{
    public static class Configurator
    {
        public static void ConfigureAndAddServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            Thread.CurrentThread.CurrentUICulture = 
                CultureInfo.GetCultureInfo(
                configuration.GetSection("AppLanguage").Value!);

            //-- Application
            services.AddApplicationServices(new DatabaseSettings(configuration));

            //-- MassTransit
            services.AddMassTransit(x =>
            {
                //x.AddConsumers();

                x.ConfigureBasedOnSettings(
                    new MassTransitSettings(configuration),
                    (context, cfg) =>
                    {
                        //cfg.ConfigureConsumers(context);
                    });

                x.AddRequestClients();
            });
        }
    }
}
