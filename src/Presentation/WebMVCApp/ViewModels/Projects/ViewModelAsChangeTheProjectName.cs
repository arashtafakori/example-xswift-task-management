using Domain.ProjectAggregation;

namespace MVCWebApp.Model.Projects
{
    public class ViewModelAsChangeTheProjectName
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }

    public static partial class ViewModelExtensions
    {
        public static ViewModelAsChangeTheProjectName 
            GetViewModelAsChangeTheProjectName(this ProjectInfo viewModel)
        {
            return new ViewModelAsChangeTheProjectName()
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };
        }

        public static ChangeTheProjectName 
            GetTheRequest(this ViewModelAsChangeTheProjectName model)
        {
            return new ChangeTheProjectName(model.Id, model.Name);
        }
    }
}
