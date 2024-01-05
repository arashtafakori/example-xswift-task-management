using XSwift.EntityFrameworkCore;

namespace Module.Persistence
{
    public class ModuleDbTransaction : DbTransaction
    {
        public ModuleDbTransaction(ModuleDbContext context) 
            : base(context) { }
    }
}