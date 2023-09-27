using EntityFrameworkCore.CoreX.Datastore;

namespace Persistence.EFCore
{
    public class ModuleEFCoreDatabase : Database
    {
        public ModuleEFCoreDatabase(ModuleDbContext context) 
            : base(context){ }
    }
}
