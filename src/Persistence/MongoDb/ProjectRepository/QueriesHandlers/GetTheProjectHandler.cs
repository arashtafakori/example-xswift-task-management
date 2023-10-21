using MediatR;
using XSwift.Datastore;
using Domain.ProjectAggregation;
using EntityFrameworkCore.XSwift.Datastore;

namespace Persistence.MongoDb.ProjectRepository
{
    internal class GetTheProjectHandler :
        IRequestHandler<GetTheProject, Project?>
    {
        private readonly Database _database;
        public GetTheProjectHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<Project?> Handle(
            GetTheProject request,
            CancellationToken cancellationToken)
        {
            return await _database.GetItemAsync<GetTheProject, Project>(request: request);
        }
    }
}
