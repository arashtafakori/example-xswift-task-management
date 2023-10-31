using MediatR;
using XSwift.Datastore;
using Domain.SprintAggregation;
using XSwift.Domain;
using EntityFrameworkCore.XSwift;

namespace Persistence.EFCore.SprintRepository
{
    public class GetSprintInfoListHandler :
        IRequestHandler<GetSprintInfoList,
            PaginatedViewModel<SprintInfo>>
    {
        private readonly Database _database;
        public GetSprintInfoListHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<PaginatedViewModel<SprintInfo>> Handle(
            GetSprintInfoList request,
            CancellationToken cancellationToken)
        {
            return await _database.GetPaginatedListAsync<
                GetSprintInfoList, SprintEntity, SprintInfo>(
                request: request,
                selector: (IQueryable<SprintEntity> query) =>
                {
                    return SprintQueryable.SelectAsSprintInfo(_database, query);
                });
        }
    }
}
