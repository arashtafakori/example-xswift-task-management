using Domain.ProjectAggregation;

namespace MVCWebApp.Model.Projects
{
    public class ViewModelAsDefineANewProject
    {
        public required string Name { get; set; }
    }

    public static partial class ViewModelExtensions
    {
        public static DefineANewProject GetTheRequest(
            this ViewModelAsDefineANewProject model)
        {
            return new DefineANewProject(model.Name);
        }
    }
}
