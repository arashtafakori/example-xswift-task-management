using XSwift.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Module.Persistence
{
    public class ModuleEFCoreDatabase : Database
    {
        public ModuleEFCoreDatabase(ModuleDbContext context) 
            : base(context) { }
    }
}
