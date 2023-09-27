using XSwift.Domain;
using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectAggregation
{
    public class ProjectInfo : ViewModel, IModifiedViewModel, IArchivableViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public bool IsArchived { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

        public static ProjectInfo? ToModel(Project? entity)
        {
            if (entity == null) return null;

            return new ProjectInfo()
            {
                Id = entity.Id,
                Name = entity.Name,
                IsArchived = Convert.ToBoolean(entity.Deleted),
                ModifiedDate = entity.ModifiedDate
            };
        }
    }
}
