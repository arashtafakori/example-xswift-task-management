using XSwift.Base;
using Module.Domain.Properties;
using MediatR;

namespace Module.Domain.TaskAggregation
{
    internal class GetTaskStatusListHandler :
        IRequestHandler<GetTaskStatusList, List<KeyValuePair<int, string>>>
    {
        public Task<List<KeyValuePair<int, string>>> Handle(
            GetTaskStatusList request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(
                EnumHelper.ToKeyValuePairList<TaskStatus>(Resource.ResourceManager));
        }
    }
}
