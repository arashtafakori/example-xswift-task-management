using CoreX.Domain;
using System.ComponentModel.DataAnnotations;

namespace Domain.SprintAggregation
{
    public class SprintInfo : ViewModel, IModifiedViewModel
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public required string Name { get; set; }
        [Display(Name = "Project")]
        public required string ProjectName { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

        public static SprintInfo? ToModel(
            Sprint? entity,
            string projectName)
        {
            if (entity == null) return null;

            return new SprintInfo()
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                Name = entity.Name,
                ProjectName = projectName,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                ModifiedDate = entity.ModifiedDate
            };
        }
    }
}
