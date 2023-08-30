using Domain.ProjectAggregation;

namespace MVCWebApp.Model
{
    public class ViewModelAsDefineANewProject
    {
        public required string Name { get; set; }

        public DefineANewProject ConvertToARequest()
        {
            return new DefineANewProject(Name);
        }
    }
}
