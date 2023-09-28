using XSwift.Domain;
using Domain.Properties;
using Domain.SprintAggregation;
using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectAggregation
{
    public class Project : Entity<Project, Guid>, IAggregateRoot
    {
        [MinLengthShouldBe(3)]
        [MaxLengthShouldBe(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; private set; } = string.Empty;

        public ICollection<Sprint> Sprints { get; }
        public ICollection<TaskAggregation.Task> Tasks { get; }

        public override ConditionProperty<Project>? Uniqueness()
        {
            return new ConditionProperty<Project>()
            {
                Condition = x => x.Name == Name,
                Description = Resource.UniquenessOfTheProject
            };
        }

        public static Project New()
        {
            return new Project();
        }
        public Project SetName(string value)
        {
            Name = value;

            return this;
        }
    }
}
