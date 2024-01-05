using XSwift.Domain;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using MediatR;
using XSwift.Base;

namespace Module.Domain.TaskAggregation
{
    public class GetTaskInfoList :
        QueryListRequest<TaskEntity, PaginatedViewModel<TaskInfo>>
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

        public override ExpressionBuilder<TaskEntity> Where()
        {
            WhereExpression.And(x => x.ProjectId == ProjectId);
            return base.Where();    
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
