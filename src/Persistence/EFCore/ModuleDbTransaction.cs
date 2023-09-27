using EntityFrameworkCore.CoreX.Datastore;

namespace Persistence.EFCore
{
    public class ModuleDbTransaction : DbTransaction
    {
        public ModuleDbTransaction(ModuleDbContext context) 
            : base(context) { }
    }
}