using CoreX.Domain;
using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectAggregation
{
    public class ProjectInfo : IModifiedViewModel, IArchivableViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public bool Deleted { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }

    public static partial class ViewModelExtensions
    {
        public static ProjectInfo? GetInfo(this Project? entity)
        {
            if(entity == null) return null;

            return new ProjectInfo()
            {
                Id = entity.Id,
                Name = entity.Name,
                Deleted = Convert.ToBoolean(entity.Deleted),
                ModifiedDate = entity.ModifiedDate
            };
        }
    }
}
