using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class GetSomeProjectInfo :
        ReadonlyRetrivalEntitiesRequest<Project>, 
        IRequest<List<ProjectInfo>>
    {
        public GetSomeProjectInfo(int offset, int limit) : 
            base(offset: offset, limit: limit)
        {
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
