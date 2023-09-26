using Domain.ProjectAggregation;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WebMVCApp.ViewModels
{
    public class DefineAProjectViewModel
    {
        [Required]
        public string Name { get; set; }

        public DefineAProject ToRequest()
        {
            return new DefineAProject(Name);
        }
    }
}
