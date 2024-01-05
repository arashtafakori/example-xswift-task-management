using MediatR;
using XSwift.Domain;

namespace Module.Domain.TaskAggregation
{
    public class GetTaskStatusList : BaseQueryRequest<List<KeyValuePair<int, string>>>
    {
    }
}
