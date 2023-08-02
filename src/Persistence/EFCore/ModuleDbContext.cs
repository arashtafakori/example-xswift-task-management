using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CoreX.Datastore;

namespace Doit.AccountModule.Persistence.EFCore
{
    public class ModuleDbContext : DbContext
    {
        //public DbSet<Account> Accounts { get; set; }
 
        public ModuleDbContext(DbContextOptions<ModuleDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof().Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddSoftDeleteCapabilityForQuery();
        }
    }
}
