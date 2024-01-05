using MediatR;
using XSwift.Datastore;
using Module.Domain.ProjectAggregation;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.ProjectRepository
{
    internal class GetTheProjectHandler :
        IRequestHandler<GetTheProject, ProjectEntity?>
    {
        private readonly Database _database;
        public GetTheProjectHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<ProjectEntity?> Handle(
            GetTheProject request,
            CancellationToken cancellationToken)
        {
            return await _database.GetItemAsync<GetTheProject, ProjectEntity>(request: request);
        }
    }
}
