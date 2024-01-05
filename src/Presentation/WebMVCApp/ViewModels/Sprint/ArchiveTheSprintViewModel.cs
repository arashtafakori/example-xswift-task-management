using XSwift.Base;
using Module.Domain.SprintAggregation;

namespace Module.Presentation.WebMVCApp.ViewModels
{
    public class ArchiveTheSprintViewModel
    {
        public SprintInfo? SprintInfo { get; set; }

        public List<IIssue>? IssuesOfArchivingPossibility { get; set; }

        public bool ArchivingAllTaskMode { get; set; } = false;

        public ArchiveTheSprint ToRequest()
        {
            return new ArchiveTheSprint(
                SprintInfo!.Id, 
                archivingAllTaskMode: ArchivingAllTaskMode);
        }
    }
}
