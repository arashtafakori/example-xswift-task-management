using EntityFrameworkCore.XSwift;
using EntityFrameworkCore.XSwift.Datastore;

namespace Persistence.EFCore
{
    public class ModuleDbTransaction : DbTransaction
    {
        public ModuleDbTransaction(ModuleDbContext context) 
            : base(context) { }
    }
}