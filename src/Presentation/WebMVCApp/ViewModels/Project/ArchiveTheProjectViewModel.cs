using XSwift.Base;
using Module.Domain.ProjectAggregation;

namespace Module.Presentation.WebMVCApp.ViewModels
{
    public class ArchiveTheProjectViewModel
    {
        public ProjectInfo? ProjectInfo { get; set; }

        public List<IIssue>? IssuesOfArchivingPossibility { get; set; }    
    }
}
