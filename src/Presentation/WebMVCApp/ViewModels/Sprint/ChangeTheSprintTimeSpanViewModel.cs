using XSwift.Base;
using Domain.SprintAggregation;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WebMVCApp.ViewModels
{
    public class ChangeTheSprintTimeSpanViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public SprintInfo? SprintInfo { get; set; }
        public static ChangeTheSprintTimeSpanViewModel ToViewModel(SprintInfo info)
        {
            return new ChangeTheSprintTimeSpanViewModel()
            {
                Id = info.Id,
                StartDate = info.StartDate ?? DateTimeHelper.UtcNow,
                EndDate = info.EndDate ?? DateTimeHelper.UtcNow,
                SprintInfo = info
            };
        }

        public ChangeTheSprintTimeSpan ToRequest()
        {
            return new ChangeTheSprintTimeSpan(Id, StartDate!, EndDate);
        }
    }
}
