using XSwift.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Domain.TaskAggregation
{
    public class GetTheTaskInfo :
        QueryItemRequestById<Task, Guid>,
        IRequest<TaskInfo?>
    {
        public GetTheTaskInfo(Guid id) : base(id)
        {
            PreventIfNoEntityWasFound = true;
        }
    }
}
