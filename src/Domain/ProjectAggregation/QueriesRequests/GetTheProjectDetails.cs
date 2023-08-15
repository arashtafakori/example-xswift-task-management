using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class GetTheProjectDetails :
        ReadonlyRetrivalEntityRequestById<Project, Guid>,
        IRequest<ProjectDetailsViewModel?>
    {
        public GetTheProjectDetails(Guid id) : base(id)
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
