using XSwift.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Domain.TaskAggregation
{
    public class GetTaskInfoList :
        QueryListRequest<Task>, 
        IRequest<PaginatedViewModel<TaskInfo>>
    {
        [Required]
        public Guid ProjectId { get; private set; }
        public Guid? SprintId { get; set; }
        public TaskStatus? Status { get; set; }
        public string? DescriptionSearchKey { get; set; }

        public GetTaskInfoList()
        {
            ValidationState.Validate();
        }
        public GetTaskInfoList SetProjectId(Guid value)
        {
            ProjectId = value;
            return this;
        }
        public override Expression<Func<Task, bool>>? Identification()
        {
            return x => x.ProjectId == ProjectId;
        }
    }
}
