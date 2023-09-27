using XSwift.Domain;
using MediatR;

namespace Domain.TaskAggregation
{
    public class GetTaskStatusList : BaseQueryRequest, 
        IRequest<List<KeyValuePair<int, string>>>
    {
    }
}
