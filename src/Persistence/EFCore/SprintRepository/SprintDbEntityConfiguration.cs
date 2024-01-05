using Module.Domain.SprintAggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Module.Persistence.SprintRepository
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
