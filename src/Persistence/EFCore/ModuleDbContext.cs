using Microsoft.EntityFrameworkCore;
using XSwift.EntityFrameworkCore;
using Module.Persistence.ProjectRepository;
using Module.Domain.ProjectAggregation;
using Module.Domain.SprintAggregation;
using Module.Domain.TaskAggregation;

namespace Module.Persistence
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
