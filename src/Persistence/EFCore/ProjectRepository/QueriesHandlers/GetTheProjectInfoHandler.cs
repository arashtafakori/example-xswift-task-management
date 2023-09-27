using MediatR;
using CoreX.Datastore;
using Domain.ProjectAggregation;
using EntityFrameworkCore.CoreX.Datastore;

namespace Persistence.EFCore.ProjectRepository
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
