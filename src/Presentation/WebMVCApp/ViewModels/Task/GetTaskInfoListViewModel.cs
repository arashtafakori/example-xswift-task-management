using XSwift.Domain;
using Domain.ProjectAggregation;
using Domain.TaskAggregation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Presentation.WebMVCApp.ViewModels
{
    public class GetTaskInfoListViewModel
    {
        [DisplayName("Serach on: Descriptions")]
        public string? DescriptionSearchKey { get; set; }
        [DisplayName("Filter by: Sprint")]
        public Guid? SprintId { get; set; }
        [DisplayName("Filter by: Status")]
        public Domain.TaskAggregation.TaskStatus? Status { get; set; }

        public ProjectInfo? ProjectInfo { get; set; }
        public List<SelectListItem>? SprintsInfoItems { get; set; }
        public List<SelectListItem>? TaskStatusSelectListItems { get; set; }
        public PaginatedViewModel<TaskInfo> TaskInfoList { get; set; }

        public GetTaskInfoList ToRequest()
        {
            var request = new GetTaskInfoList
            {
                SprintId = SprintId,
                Status = Status,
                DescriptionSearchKey = DescriptionSearchKey
            };

            return request;
        }
    }
}
