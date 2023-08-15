using CoreX.Base;
using CoreX.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Domain.ProjectAggregation
{
    public class GetSomeProjectDetails :
        ReadonlyRetrivalEntitiesRequest<Project>, 
        IRequest<List<ProjectDetailsViewModel>>
    {
        public int Limit { get; internal set; }
        public int Offset { get; internal set; }

        public GetSomeProjectDetails(
            int limit = 20,
            int offset = 0)
        {
            Limit = limit;
            Offset = offset;

            ValidationState.Validate();
        }

        /// <summary>
        /// "To grant the permission for deleted items.".
        /// </summary>
        /// <returns></returns>
        public bool OnIncludingArchivedDataConfiguration()
        {
            return true;
        }

        //public override Expression<Func<Project, bool>> DeclareGeneralCondition()
        //{

        //    return new ExpressionBuilder<Project>().GetExpression();
        //}
    }
}
