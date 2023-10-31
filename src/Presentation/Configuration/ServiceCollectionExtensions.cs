using XSwift.MassTransit;
using MassTransit;
using XSwift.Settings;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application;
using EntityFrameworkCore.XSwift;

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
                massTransitSettings: new MassTransitSettings(configuration),
                databaseSettings: new DatabaseSettings(configuration),
                inMemoryDatabaseSettings: new InMemoryDatabaseSettings(configuration),
                sqlServerSettings: new SqlServerSettings(configuration));
        }

        public static void ConfigureAndAddServices(
            this IServiceCollection services,
            string appLanguage,
            MassTransitSettings massTransitSettings,
            DatabaseSettings databaseSettings,
            InMemoryDatabaseSettings? inMemoryDatabaseSettings = null,
            SqlServerSettings? sqlServerSettings = null)
        {
            Thread.CurrentThread.CurrentUICulture =
                CultureInfo.GetCultureInfo(appLanguage);

            //-- Application
            services.AddApplicationServices(
                databaseSettings, 
                inMemoryDatabaseSettings,
                sqlServerSettings);

            //-- MassTransit
            services.AddMassTransit(x =>
            {
                //x.AddConsumers();

                x.ConfigureBasedOnSettings(massTransitSettings!,
                    (context, cfg) =>
                    {
                        //cfg.ConfigureConsumers(context);
                    });

                x.AddRequestClients();
            });
        }
    }
}
