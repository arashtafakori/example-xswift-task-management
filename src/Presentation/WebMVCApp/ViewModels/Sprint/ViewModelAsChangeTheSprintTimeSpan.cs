using CoreX.Base;
using Domain.SprintAggregation;
using System.ComponentModel.DataAnnotations;

namespace MVCWebApp.Model
{
    public class ViewModelAsChangeTheSprintTimeSpan
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public static ViewModelAsChangeTheSprintTimeSpan GetViewModel(SprintInfo viewModel)
        {
            return new ViewModelAsChangeTheSprintTimeSpan()
            {
                Id = viewModel.Id,
                StartDate = viewModel.StartDate ?? DateTimeHelper.UtcNow,
                EndDate = viewModel.EndDate ?? DateTimeHelper.UtcNow,
            };
        }

        public ChangeTheSprintTimeSpan ConvertToARequest()
        {
            return new ChangeTheSprintTimeSpan(Id, StartDate!, EndDate);
        }
    }
}
