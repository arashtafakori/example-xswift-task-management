using XSwift.Base;
using Domain.ProjectAggregation;

namespace Presentation.WebMVCApp.ViewModels
{
    public class ArchiveTheProjectViewModel
    {
        public ProjectInfo? ProjectInfo { get; set; }

        public List<IIssue>? Issues { get; set; }    
    }
}
