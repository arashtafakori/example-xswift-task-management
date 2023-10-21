using XSwift.Domain;
using Domain.Properties;
using System.ComponentModel.DataAnnotations;
using Domain.ProjectAggregation;
using Domain.TaskAggregation;

namespace Domain.SprintAggregation
{
    public class SprintEntity : Entity<SprintEntity, Guid>, IAggregateRoot
    {
        [MinLengthShouldBe(3)]
        [MaxLengthShouldBe(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; protected set; } = string.Empty;

        public DateTime? StartDate { get; protected set; }

        public DateTime? EndDate { get; protected set; }

        [Required]
        public Guid ProjectId { get; protected set; }
        public override ConditionProperty<SprintEntity>? Uniqueness()
        {
            return new ConditionProperty<SprintEntity>()
            {
                Condition = x => x.ProjectId == ProjectId && x.Name == Name,
                Description = Resource.UniquenessOfTheSprint
            };
        }
        public static SprintEntity New() { return new SprintEntity(); }
        public SprintEntity SetProjectId(Guid value)
        {
            ProjectId = value;

            return this;
        }
        public SprintEntity SetName(string value)
        {
            Name = value;

            return this;
        }

        public SprintEntity SetStartDate(DateTime value)
        {
            StartDate = value;

            return this;
        }
        public SprintEntity SetEndDate(DateTime value)
        {
            EndDate = value;

            return this;
        }

        #region These fields belong to EFCore DbContext configurations
        public ProjectEntity Project { get; private set; }
        public ICollection<TaskEntity> Tasks { get; }
        #endregion
    }
}
