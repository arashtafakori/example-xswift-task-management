using XSwift.Base;
using Domain.Properties;
using MediatR;

namespace Domain.TaskAggregation
{
    internal class GetTaskStatusListHandler :
        IRequestHandler<GetTaskStatusList, List<KeyValuePair<int, string>>>
    {
        public Task<List<KeyValuePair<int, string>>> Handle(
            GetTaskStatusList request,
            CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(
                EnumHelper.ToKeyValuePairList<TaskStatus>(Resource.ResourceManager));
        }
    }
}
