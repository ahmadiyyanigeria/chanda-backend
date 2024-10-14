using Application.Repositories;
using Mapster;
using MediatR;
using System.Collections.Generic;
using static Application.Commands.CreateReminder;

namespace Application.Queries
{
    public class GetReminders
    {
        public record Query(Guid MemberId): IRequest<IReadOnlyList<ReminderResponse>>;

        public class Handler : IRequestHandler<Query, IReadOnlyList<ReminderResponse>>
        {
            private readonly IReminderRepository _repository;

            public Handler(IReminderRepository repository)
            {
                _repository = repository;
            }

            public async Task<IReadOnlyList<ReminderResponse>> Handle(Query query, CancellationToken cancellationToken)
            {
                var reminders = await _repository.GetMemberReminders(query.MemberId);

                return reminders.Adapt<IReadOnlyList<ReminderResponse>> ();
            }
        }
    }
}
