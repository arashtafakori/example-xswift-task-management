using Domain.SprintAggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EFCore.SprintRepository
{
    public class SprintDbEntityConfiguration : IEntityTypeConfiguration<SprintEntity>
    {
        public void Configure(EntityTypeBuilder<SprintEntity> builder)
        {
            builder.ToTable(nameof(ModuleDbContext.Sprints));
            builder.HasKey(x => x.Id);
        }
    }
}
