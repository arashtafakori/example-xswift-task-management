using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class GetTheProjectInfo :
        ReadonlyRetrivalEntityRequestById<Project, Guid>,
        IRequest<ProjectInfo?>
    {
        public GetTheProjectInfo(Guid id) : base(id)
        {
            ThrowExceptionIfEntityWasNotFound = true;
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
