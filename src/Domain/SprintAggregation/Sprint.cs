using CoreX.Domain;
using Domain.ProjectAggregation;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

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

        public override Expression<Func<Sprint, bool>>? UniqueSpecification()
        {
            return x => x.ProjectId == ProjectId && x.Name == Name;
        }
        public static Sprint New()
        {
            return new Sprint();
        }
        public Sprint SetProjectId(Guid projectId)
        {
            ProjectId = projectId;

            return this;
        }
        public Sprint SetName(string name)
        {
            Name = name;

            return this;
        }

        public Sprint SetStartDate(DateTime date)
        {
            StartDate = date;

            return this;
        }
        public Sprint SetEndDate(DateTime date)
        {
            EndDate = date;

            return this;
        }
    }
}
