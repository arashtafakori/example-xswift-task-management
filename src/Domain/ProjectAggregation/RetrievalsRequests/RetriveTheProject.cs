using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class RetriveTheProject :
        RetrivalEntityRequestById<Project, Guid>,
        IRequest<Project?>
    {
        
        public RetriveTheProject(Guid id,
            bool trackingMode = true,
            bool evenArchivedData= false)
            : base(id,
                  trackingMode: trackingMode,
                  evenArchivedData: evenArchivedData)
        {
            ThrowExceptionIfEntityWasNotFound = true;
        }
    }
}
