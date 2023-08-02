using EntityFrameworkCore.CoreX.Datastore;

namespace Doit.AccountModule.Persistence.EFCore
{
    public class ModuleEFCoreDatabase : Database
    {
        public ModuleEFCoreDatabase(ModuleDbContext context) 
            : base(context){ }
    }
}
