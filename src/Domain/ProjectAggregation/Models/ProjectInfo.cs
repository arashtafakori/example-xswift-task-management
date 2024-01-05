using XSwift.Domain;
using System.ComponentModel.DataAnnotations;

namespace Module.Domain.ProjectAggregation
{
    public class ProjectInfo : ViewModel, IModifiedViewModel, IArchivableViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public bool IsArchived { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

        public static ProjectInfo? ToModel(ProjectEntity? project)
        {
            if (project == null) return null;

            return new ProjectInfo()
            {
                Id = project.Id,
                Name = project.Name,
                IsArchived = Convert.ToBoolean(project.Deleted),
                ModifiedDate = project.ModifiedDate
            };
        }
    }
}
