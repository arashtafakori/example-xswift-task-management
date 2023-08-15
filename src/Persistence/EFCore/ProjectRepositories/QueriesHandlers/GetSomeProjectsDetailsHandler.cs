using MediatR;
using CoreX.Datastore;
using Domain.ProjectAggregation;
using EntityFrameworkCore.CoreX.Datastore;

namespace Persistence.EFCore.ProjectRepository
{
    public class GetSomeProjectsDetailsHandler :
        IRequestHandler<GetSomeProjectDetails,
            List<ProjectDetailsViewModel>>
    {
        private readonly Database _database;
        public GetSomeProjectsDetailsHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<List<ProjectDetailsViewModel>> Handle(
            GetSomeProjectDetails request,
            CancellationToken cancellationToken)
        {
            return (await _database.GetEntitiesAsync<
                GetSomeProjectDetails, Project>(request: request))
                .ConvertAll(x => x.ToViewModel())!;
        }
    }
}
