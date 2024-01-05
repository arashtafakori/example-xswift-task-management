using Module.Domain.SprintAggregation;

namespace Module.Presentation.WebMVCApp.ViewModels
{
    public class ChangeTheSprintNameViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public SprintInfo? SprintInfo { get; set; }
        public static ChangeTheSprintNameViewModel ToViewModel(SprintInfo info)
        {
            return new ChangeTheSprintNameViewModel()
            {
                Id = info.Id,
                Name = info.Name,
                SprintInfo = info,
            };
        }

        public ChangeTheSprintName ToRequest()
        {
            return new ChangeTheSprintName(Id, Name);
        }
    }
}
