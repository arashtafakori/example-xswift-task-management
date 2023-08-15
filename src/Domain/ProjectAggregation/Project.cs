using CoreX.Domain;
using CoreX.Domain.PredefinedFieldValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Domain.ProjectAggregation
{
    public class Project : Entity<Project, Guid>, IAggregateRoot
    {
        [MinLengthShouldBe(3)]
        [MaxLengthShouldBe(50)]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; private set; } = string.Empty;
        public override Expression<Func<Project, bool>>? UniqueSpecification()
        {
            return x => x.Name == Name;
        }
        public static Project New()
        {
            return new Project();
        }
        public Project SetName(string name)
        {
            Name = name;

            return this;
        }
    }
}
