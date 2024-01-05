using MediatR;
using XSwift.Datastore;
using Module.Domain.ProjectAggregation;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.ProjectRepository
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
                GetTheProjectInfo, ProjectEntity, ProjectInfo>(
                request: request,
                converter : ProjectInfo.ToModel!);
        }
    }
}
