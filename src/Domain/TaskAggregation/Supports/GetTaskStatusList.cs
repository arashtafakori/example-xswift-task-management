using MediatR;
using XSwift.Domain;

namespace Domain.TaskAggregation
{
    public class GetTaskStatusList : BaseQueryRequest<List<KeyValuePair<int, string>>>
    {
    }
}
