using XSwift.Domain;
using MediatR;
using XSwift.Base;

namespace Module.Domain.TaskAggregation
{
    internal class SetTheTasksOfTheSprintToNoSprint :
        BulkCommandRequest<TaskEntity, List<Guid>>
    {
        public Guid SprintId { get; private set; }

        public SetTheTasksOfTheSprintToNoSprint()
        {
            ValidationState.Validate();
        }

        public SetTheTasksOfTheSprintToNoSprint SetSprintId(Guid value)
        {
            SprintId = value;
            return this;
        }

        public override ExpressionBuilder<TaskEntity> Where()
        {
            WhereExpression.And(x => x.SprintId == SprintId);
            return base.Where();
        }

        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }

        public override async Task NextAsync(
            IMediator mediator, List<Guid> tasksIds)
        {
            foreach (Guid taskId in tasksIds)
                await mediator.Send(new ArchiveTheTask(taskId));
        }
    }
}
