using CoreX.Base;
using CoreX.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Domain.ProjectAggregation
{
    public class PreventIfThereIsAnProjectWithThisName :
        AnyRequest<Project>,
        IInvariant,
        IRequest<bool>
    {
        [BindTo(typeof(Project), nameof(Project.Name))]
        public string Name { get; private set; }
        public PreventIfThereIsAnProjectWithThisName
            (string name)
        {
            Name = name.Trim();
        }

        public async Task<bool> CheckAsync(IMediator mediator)
        {
           return await mediator.Send(this);
        }

        public IIssue? GetIssue()
        {
            return new AnProjectWithThisNameHasAlreadyBeenDefinedIssue();
        }

        public override Expression<Func<Project, bool>> Condition()
        {
            return x => x.Name == Name;
        }
    }
}
