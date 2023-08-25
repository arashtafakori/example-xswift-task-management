using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class GetSomeProjectInfo :
        ReadonlyRetrivalEntitiesRequest<Project>, 
        IRequest<List<ProjectInfo>>
    {
        public int Limit { get; internal set; }
        public int Offset { get; internal set; }

        public GetSomeProjectInfo(
            int limit = 20,
            int offset = 0)
        {
            Limit = limit;
            Offset = offset;

            ValidationState.Validate();
        }

        /// <summary>
        /// "To grant the permission for deleted items.".
        /// </summary>
        /// <returns></returns>
        public bool OnIncludingArchivedDataConfiguration()
        {
            return true;
        }
    }
}
