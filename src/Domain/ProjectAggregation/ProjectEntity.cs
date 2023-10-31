using XSwift.Domain;
using Domain.Properties;
using System.ComponentModel.DataAnnotations;
using Domain.SprintAggregation;
using Domain.TaskAggregation;
using System.Text.Json.Serialization;

namespace Domain.ProjectAggregation
{
    public class ProjectEntity : Entity<ProjectEntity, Guid>, IAggregateRoot
    {
        [MinLengthShouldBe(3)]
        [MaxLengthShouldBe(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; protected set; } = string.Empty;
        public static ProjectEntity New() { return new ProjectEntity(); }

        public override ConditionProperty<ProjectEntity>? Uniqueness()
        {
            return new ConditionProperty<ProjectEntity>()
            {
                Condition = x => x.Name == Name,
                Description = Resource.UniquenessOfTheProject
            };
        }

        public ProjectEntity SetName(string value)
        {
            Name = value;

            return this;
        }

        #region These fields belong to EFCore DbContext configurations
        public ICollection<SprintEntity> Sprints { get; }
        public ICollection<TaskEntity> Tasks { get; }
        #endregion
    }
}
