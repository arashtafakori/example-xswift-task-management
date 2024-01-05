using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Module.Persistence;
using XSwift.Settings;

namespace Module.Application
{
    public class DatabaseInitialization
    {
        private readonly IServiceCollection _services;
        private readonly DatabaseSetting _databaseSetting;
        public DatabaseInitialization(IServiceCollection services, DatabaseSetting databaseSetting) {
            _services = services;
            _databaseSetting = databaseSetting;
        }

        public void Initialize()
        {
            if(_databaseSetting.IsInMemory)
            {
                SeedData();
            }
            else
            {
                var serviceProvider = _services.BuildServiceProvider();

                using var serviceScope = serviceProvider.
                    GetRequiredService<IServiceScopeFactory>().CreateScope();

                var dbContext = serviceScope.ServiceProvider.GetService<ModuleDbContext>();
                if (!dbContext!.Database.GetAppliedMigrations().Any())
                {
                    dbContext!.Database.Migrate();
                    SeedData();
                }
            }
        }

        private void SeedData()
        {

        }
    }
}
