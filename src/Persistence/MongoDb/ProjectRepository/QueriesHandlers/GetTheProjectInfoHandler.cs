using MediatR;
using XSwift.Datastore;
using Domain.ProjectAggregation;
using EntityFrameworkCore.XSwift.Datastore;

namespace Persistence.MongoDb.ProjectRepository
{
    public class GetTheProjectInfoHandler :
        IRequestHandler<GetTheProjectInfo, ProjectInfo?>
    {
        private readonly Database _database;
        public GetTheProjectInfoHandler(IDatabase database) 
        {
            _database = (Database)database;
        }

        public async Task<ProjectInfo?> Handle(
            GetTheProjectInfo request,
            CancellationToken cancellationToken)
        {
            return await _database.GetItemAsync<
                GetTheProjectInfo, Project, ProjectInfo>(
                request: request,
                converter : ProjectInfo.ToModel!);
        }
    }
}
