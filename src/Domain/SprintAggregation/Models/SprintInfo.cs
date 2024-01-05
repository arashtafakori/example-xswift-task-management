using XSwift.Domain;
using System.ComponentModel.DataAnnotations;

namespace Module.Domain.SprintAggregation
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
            SprintEntity? sprint,
            string projectName)
        {
            if (sprint == null) return null;

            return new SprintInfo()
            {
                Id = sprint.Id,
                ProjectId = sprint.ProjectId,
                Name = sprint.Name,
                ProjectName = projectName,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                ModifiedDate = sprint.ModifiedDate
            };
        }
    }
}
