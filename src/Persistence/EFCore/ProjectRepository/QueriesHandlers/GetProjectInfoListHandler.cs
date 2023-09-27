using MediatR;
using XSwift.Datastore;
using Domain.ProjectAggregation;
using EntityFrameworkCore.XSwift.Datastore;
using XSwift.Domain;

namespace Persistence.EFCore.ProjectRepository
{
    public class GetProjectInfoListHandler :
        IRequestHandler<GetProjectInfoList,
            PaginatedViewModel<ProjectInfo>>
    {
        private readonly Database _database;
        public GetProjectInfoListHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<PaginatedViewModel<ProjectInfo>> Handle(
            GetProjectInfoList request,
            CancellationToken cancellationToken)
        {
            return await _database.GetPaginatedListAsync<
                GetProjectInfoList, Project, ProjectInfo>(
                request: request, converter: ProjectInfo.ToModel!);
        }
    }
}
