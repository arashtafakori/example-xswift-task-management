using CoreX.Domain;
using System.ComponentModel.DataAnnotations;

namespace Domain.ProjectAggregation
{
    public class ProjectDetailsViewModel : IModifiedView, IArchivableView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }

    public static partial class ViewModelExtensions
    {
        public static ProjectDetailsViewModel? ToViewModel(this Project? entity)
        {
            if(entity == null) return null;

            return new ProjectDetailsViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Deleted = Convert.ToBoolean(entity.Deleted),
                ModifiedDate = entity.ModifiedDate
            };
        }
    }
}
