using XSwift.Domain;
using MediatR;
using System.Linq.Expressions;


namespace Domain.TaskAggregation
{
    internal class SetTheTasksOfTheSprintToNoSprint :
        BulkCommandRequest<Task, Guid>,
        IRequest
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
        public override Expression<Func<Task, bool>>? Identification()
        {
            return x => x.SprintId == SprintId;
        }

        public override  async System.Threading.Tasks.Task ResolveAsync(
            List<Guid> tasksIds, IMediator mediator)  
        {
            foreach (Guid taskId in tasksIds)
              await mediator.Send(new ArchiveTheTask(taskId));
        }
    }
}
