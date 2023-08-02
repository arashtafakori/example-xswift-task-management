using EntityFrameworkCore.CoreX.Datastore;

namespace Doit.AccountModule.Persistence.EFCore
{
    public class ModuleDbTransaction : DbTransaction
    {
        public ModuleDbTransaction(ModuleDbContext context) 
            : base(context) { }
    }
}