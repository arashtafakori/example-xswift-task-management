using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CoreX.Datastore;
using Domain.ProjectAggregation;
using Persistence.EFCore.ProjectRepository;
using Domain.SprintAggregation;
using MassTransit.Transports;

namespace Persistence.EFCore
{
    public class ModuleDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Domain.TaskAggregation.Task> Tasks { get; set; }
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
