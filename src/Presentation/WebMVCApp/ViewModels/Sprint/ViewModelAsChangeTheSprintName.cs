using Domain.SprintAggregation;

namespace MVCWebApp.Model
{
    public class ViewModelAsChangeTheSprintName
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public static ViewModelAsChangeTheSprintName GetViewModel(SprintInfo viewModel)
        {
            return new ViewModelAsChangeTheSprintName()
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };
        }

        public ChangeTheSprintName ConvertToARequest()
        {
            return new ChangeTheSprintName(Id, Name);
        }
    }
}
