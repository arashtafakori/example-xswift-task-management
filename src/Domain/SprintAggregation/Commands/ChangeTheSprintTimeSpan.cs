using XSwift.Base;
using XSwift.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.SprintAggregation
{
    public class ChangeTheSprintTimeSpan :
        RequestToUpdateById<Sprint, Guid>, IRequest
    {
        [BindTo(typeof(Sprint), nameof(Sprint.StartDate))]
        [Required]
        public DateTime StartDate { get; private set; }

        [BindTo(typeof(Sprint), nameof(Sprint.EndDate))]
        [Required]
        public DateTime EndDate { get; private set; }

        public ChangeTheSprintTimeSpan(
            Guid id, DateTime startDate, DateTime endDate) : base(id)
        {
            StartDate = startDate;
            EndDate = endDate;

            ValidationState.AddAnValidation(
                new PreventIfStartDateIsLaterThanEndDate<Sprint>
                (StartDate, EndDate));

            ValidationState.Validate();
        }

        public override async Task<Sprint> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var lastYear = DateTimeHelper.UtcNow.AddYears(-1);
            InvariantState.DefineAnInvariant(
                condition: () => { return StartDate < lastYear || EndDate < lastYear; },
                issue: new TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths());

            await InvariantState.CheckAsync(mediator);

            var entity = await mediator.Send(new GetTheSprint(Id));
            entity!.SetStartDate(StartDate).SetEndDate(EndDate);
            await base.ResolveAsync(mediator, entity!);
            return entity!;
        }
    }
}
