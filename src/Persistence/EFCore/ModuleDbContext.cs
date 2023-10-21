using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.XSwift.Datastore;
using Persistence.EFCore.ProjectRepository;
using Domain.ProjectAggregation;
using Domain.SprintAggregation;
using Domain.TaskAggregation;

namespace Persistence.EFCore
{
    public class ModuleDbContext : DbContext
    {
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<SprintEntity> Sprints { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public ModuleDbContext(DbContextOptions<ModuleDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectDbEntityConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddSoftDeleteCapabilityForQuery();
        }
    }
}
