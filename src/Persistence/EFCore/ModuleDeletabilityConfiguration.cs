using CoreX.Datastore;
using CoreX.Domain;
using EntityFrameworkCore.CoreX.Datastore;
using SoftDeleteServices.Configuration;

namespace Doit.AccountModule.Persistence.EFCore
{
    public class ModuleDeletabilityConfiguration :
        CascadeSoftDeleteConfiguration<ISoftDelete>
    {
        public ModuleDeletabilityConfiguration(
            ModuleDbContext context): base(context)
        {
            this.AddSoftDeleteConfiguration();
        }
    }

    internal class ModuleDeletabilityConfigurationFactory
    {
        public ModuleDeletabilityConfiguration CreateInstance(
            IDatabase _database)
        {
            return new ModuleDeletabilityConfiguration(
                _database.GetDbContext<ModuleDbContext>());
        }
    }
}
