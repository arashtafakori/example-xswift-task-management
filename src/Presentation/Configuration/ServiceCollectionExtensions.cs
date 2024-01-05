using XSwift.MassTransit;
using MassTransit;
using XSwift.Settings;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Module.Application;
using XSwift.EntityFrameworkCore;

namespace Module.Presentation.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            services.ConfigureApplicationServices(
                massTransitSetting: new MassTransitSetting(configuration),
                databaseSetting: new DatabaseSetting(configuration),
                inMemoryDatabaseSetting: new InMemoryDatabaseSetting(configuration),
                sqlServerSetting: new SqlServerSetting(configuration));
        }

        public static void ConfigureApplicationServices(
            this IServiceCollection services,
            MassTransitSetting massTransitSetting,
            DatabaseSetting databaseSetting,
            InMemoryDatabaseSetting? inMemoryDatabaseSetting = null,
            SqlServerSetting? sqlServerSetting = null)
        {
            //-- Application
            services.AddApplicationServices(
                databaseSetting, 
                inMemoryDatabaseSetting,
                sqlServerSetting);

            //-- MassTransit
            services.AddMassTransit(x =>
            {
                //x.AddConsumers();

                x.ConfigureBasedOnSettings(massTransitSetting!,
                    (context, cfg) =>
                    {
                        //cfg.ConfigureConsumers(context);
                    });

                x.AddRequestClients();
            });
        }

        public static void ConfigureLanguage(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            services.ConfigureLanguage(
                appLanguage: configuration.GetSection("AppLanguage").Value!);
        }

        public static void ConfigureLanguage(
            this IServiceCollection services,
            string appLanguage)
        {
            Thread.CurrentThread.CurrentUICulture =
                CultureInfo.GetCultureInfo(appLanguage);
        }
    }
}
