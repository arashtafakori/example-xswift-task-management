using MediatR;
using CoreX.Datastore;
using Domain.ProjectAggregation;
using EntityFrameworkCore.CoreX.Datastore;

namespace Persistence.EFCore.ProjectRepository
{
    public class GetSomeProjectsInfoHandler :
        IRequestHandler<GetSomeProjectInfo,
            List<ProjectInfo>>
    {
        private readonly Database _database;
        public GetSomeProjectsInfoHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<List<ProjectInfo>> Handle(
            GetSomeProjectInfo request,
            CancellationToken cancellationToken)
        {
            return (await _database.GetEntitiesAsync<
                GetSomeProjectInfo, Project>(request: request))
                .ConvertAll(x => x.GetInfo())!;
        }
    }
}
