using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CoreX.Datastore;
using Domain.ProjectAggregation;
using Persistence.EFCore.ProjectRepository;

namespace Persistence.EFCore
{
    public class ModuleDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Project> Sprints { get; set; }
        public ModuleDbContext(DbContextOptions<ModuleDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectEntityTypeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddSoftDeleteCapabilityForQuery();
        }
    }
}
