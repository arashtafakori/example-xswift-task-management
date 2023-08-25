using CoreX.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    public class RetriveTheSprint :
        RetrivalEntityRequestById<Sprint, Guid>,
        IRequest<Sprint?>
    {
        
        public RetriveTheSprint(Guid id,
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
