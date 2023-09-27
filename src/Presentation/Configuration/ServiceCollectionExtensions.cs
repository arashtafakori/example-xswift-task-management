using XSwift.MassTransit;
using MassTransit;
using XSwift.Settings;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application;

namespace Presentation.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAndAddServices(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            services.ConfigureAndAddServices(
                appLanguage: configuration.GetSection("AppLanguage").Value!,
                databaseSettings: new DatabaseSettings(configuration),
                massTransitSettings: new MassTransitSettings(configuration));
        }

        public static void ConfigureAndAddServices(
            this IServiceCollection services,
            string appLanguage,
            DatabaseSettings databaseSettings,
            MassTransitSettings massTransitSettings)
        {
            Thread.CurrentThread.CurrentUICulture =
                CultureInfo.GetCultureInfo(appLanguage);

            //-- Application
            services.AddApplicationServices(databaseSettings);

            //-- MassTransit
            services.AddMassTransit(x =>
            {
                //x.AddConsumers();

                x.ConfigureBasedOnSettings(massTransitSettings,
                    (context, cfg) =>
                    {
                        //cfg.ConfigureConsumers(context);
                    });

                x.AddRequestClients();
            });
        }
    }
}
