using CoreX.Domain;
using System.ComponentModel.DataAnnotations;

namespace Domain.SprintAggregation
{
    public class SprintInfo : IModifiedViewModel, IArchivableViewModel
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public required string Name { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public bool Deleted { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }

    public static partial class ViewModelExtensions
    {
        public static SprintInfo? GetInfo(this Sprint? entity)
        {
            if(entity == null) return null;

            return new SprintInfo()
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                Name = entity.Name,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Deleted = Convert.ToBoolean(entity.Deleted),
                ModifiedDate = entity.ModifiedDate
            };
        }
    }
}
