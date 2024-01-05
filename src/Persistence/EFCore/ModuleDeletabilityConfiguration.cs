using XSwift.Datastore;
using XSwift.Domain;
using XSwift.EntityFrameworkCore;
using SoftDeleteServices.Configuration;

namespace Module.Persistence
{
    public class ModuleDeletabilityConfiguration :
        CascadeSoftDeleteConfiguration<ISoftDelete>
    {
        public ModuleDeletabilityConfiguration(
            ModuleDbContext context): base(context)
        {
            this.ConfigureSoftDelete();
        }
    }

    public class ModuleDeletabilityConfigurationFactory
    {
        public ModuleDeletabilityConfiguration CreateInstance(
            IDatabase _database)
        {
            return new ModuleDeletabilityConfiguration(
                _database.GetDbContext<ModuleDbContext>());
        }
    }
}
