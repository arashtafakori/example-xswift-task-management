using MediatR;
using XSwift.Datastore;
using Module.Domain.ProjectAggregation;
using XSwift.Domain;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.ProjectRepository
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
                GetProjectInfoList, ProjectEntity, ProjectInfo>(
                request: request, converter: ProjectInfo.ToModel!);
        }
    }
}
