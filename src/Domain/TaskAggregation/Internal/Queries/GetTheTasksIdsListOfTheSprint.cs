using CoreX.Domain;
using MediatR;
using System.Linq.Expressions;


namespace Domain.TaskAggregation
{
    internal class GetTheTasksIdsListOfTheSprint :
        QueryListRequest<Task>,
        IRequest<List<Guid>>
    {
        public Guid SprintId { get; private set; }

        public GetTheTasksIdsListOfTheSprint()
        {
            ValidationState.Validate();
        }

        public GetTheTasksIdsListOfTheSprint SetSprintId(Guid value)
        {
            SprintId = value;
            return this;
        }
        public override Expression<Func<Task, bool>>? Condition()
        {
            return x => x.SprintId == SprintId;
        }
    }
}
