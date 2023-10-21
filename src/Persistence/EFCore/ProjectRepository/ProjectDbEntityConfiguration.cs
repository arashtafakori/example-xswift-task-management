using Domain.ProjectAggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EFCore.ProjectRepository
{
    public class ProjectDbEntityConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.ToTable(nameof(ModuleDbContext.Projects));
            builder.HasKey(x => x.Id);

            builder.HasMany(e => e.Sprints)
                .WithOne(e => e.Project)
                .HasForeignKey(e => e.ProjectId)
                .HasPrincipalKey(e => e.Id);
        }
    }
}
