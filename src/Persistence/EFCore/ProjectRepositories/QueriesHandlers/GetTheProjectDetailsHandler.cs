using MediatR;
using CoreX.Datastore;
using Domain.ProjectAggregation;
using EntityFrameworkCore.CoreX.Datastore;

namespace Persistence.EFCore.ProjectRepository
{
    public class GetTheProjectDetailsHandler :
        IRequestHandler<GetTheProjectDetails, ProjectDetailsViewModel?>
    {
        private readonly Database _database;
        public GetTheProjectDetailsHandler(IDatabase database) 
        {
            _database = (Database)database;
        }

        public async Task<ProjectDetailsViewModel?> Handle(
            GetTheProjectDetails request,
            CancellationToken cancellationToken)
        {
            return (await _database.GetEntityAsync<
                GetTheProjectDetails, Project>(request: request)).
                ToViewModel();
        }
    }
}
