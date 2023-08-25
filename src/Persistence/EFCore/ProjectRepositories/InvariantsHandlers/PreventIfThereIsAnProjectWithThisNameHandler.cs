//using MediatR;
//using CoreX.Datastore;
//using Domain.ProjectAggregation;
//using EntityFrameworkCore.CoreX.Datastore;

//namespace Persistence.EFCore.ProjectRepository
//{
//    public class PreventIfThereIsAnProjectWithThisNameHandler :
//        IRequestHandler<PreventIfThereIsAnProjectWithThisName, bool>
//    {
//        private readonly Database _database;
//        public PreventIfThereIsAnProjectWithThisNameHandler(
//            IDatabase database)
//        {
//            _database = (Database)database;
//        }
//        public async Task<bool> Handle(
//            PreventIfThereIsAnProjectWithThisName request,
//            CancellationToken cancellationToken)
//        {
//            return await _database.AnyAsync<
//                    PreventIfThereIsAnProjectWithThisName, Project>
//                    (request: request);
//        }
//    }
//}
