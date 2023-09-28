using XSwift.Domain;
using Domain.ProjectAggregation;
using Domain.Properties;
using System.ComponentModel.DataAnnotations;

namespace Domain.SprintAggregation
{
    public class Sprint : Entity<Sprint, Guid>, IAggregateRoot
    {
        [MinLengthShouldBe(3)]
        [MaxLengthShouldBe(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; private set; } = string.Empty;

        public DateTime? StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        [Required]
        public Guid ProjectId { get; private set; }
        public Project Project { get; private set; }

        public ICollection<TaskAggregation.Task> Tasks { get; }
        public override ConditionProperty<Sprint>? Uniqueness()
        {
            return new ConditionProperty<Sprint>()
            {
                Condition = x => x.ProjectId == ProjectId && x.Name == Name,
                Description = Resource.UniquenessOfTheSprint
            };
        }
        public static Sprint New()
        {
            return new Sprint();
        }
        public Sprint SetProjectId(Guid value)
        {
            ProjectId = value;

            return this;
        }
        public Sprint SetName(string value)
        {
            Name = value;

            return this;
        }

        public Sprint SetStartDate(DateTime value)
        {
            StartDate = value;

            return this;
        }
        public Sprint SetEndDate(DateTime value)
        {
            EndDate = value;

            return this;
        }
    }
}
