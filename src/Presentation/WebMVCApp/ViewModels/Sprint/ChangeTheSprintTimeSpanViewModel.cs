using CoreX.Base;
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
        public static ChangeTheSprintTimeSpanViewModel ToViewModel(SprintInfo sprintInfo)
        {
            return new ChangeTheSprintTimeSpanViewModel()
            {
                Id = sprintInfo.Id,
                StartDate = sprintInfo.StartDate ?? DateTimeHelper.UtcNow,
                EndDate = sprintInfo.EndDate ?? DateTimeHelper.UtcNow,
                SprintInfo = sprintInfo
            };
        }

        public ChangeTheSprintTimeSpan ToRequest()
        {
            return new ChangeTheSprintTimeSpan(Id, StartDate!, EndDate);
        }
    }
}
