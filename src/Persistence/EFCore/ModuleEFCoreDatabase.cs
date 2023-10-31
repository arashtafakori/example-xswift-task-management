using EntityFrameworkCore.XSwift;
using EntityFrameworkCore.XSwift.Datastore;

namespace Persistence.EFCore
{
    public class ModuleEFCoreDatabase : Database
    {
        public ModuleEFCoreDatabase(ModuleDbContext context) 
            : base(context){ }
    }
}
