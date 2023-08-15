using MediatR;
using CoreX.Datastore;
using Domain.ProjectAggregation;
using EntityFrameworkCore.CoreX.Datastore;

namespace Persistence.EFCore.ProjectRepository
{
    public class RetriveTheProjectHandler :
        IRequestHandler<RetriveTheProject, Project?>
    {
        private readonly Database _database;
        public RetriveTheProjectHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<Project?> Handle(
            RetriveTheProject request,
            CancellationToken cancellationToken)
        {
            return await _database.GetEntityAsync<
                RetriveTheProject, Project>(request: request);
        }
    }
}
