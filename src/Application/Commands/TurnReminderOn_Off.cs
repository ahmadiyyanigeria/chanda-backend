using Application.Contracts;
using Application.Exceptions;
using Application.Repositories;
using Domain.Exceptions;
using Mapster;
using MediatR;
using static Application.Commands.CreateReminder;

namespace Application.Commands
{
    public class TurnReminderOn_Off
    {
        public record Command(Guid Id, bool TurnOn): IRequest<ReminderResponse>;

        public class Handler : IRequestHandler<Command, ReminderResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUser _currentUser;
            private readonly IReminderRepository _reminderRepo;
            private readonly IReminderService _reminderService;

            public Handler(IUnitOfWork unitOfWork, ICurrentUser currentUser, IReminderRepository reminderRepo, IReminderService reminderService)
            {
                _unitOfWork = unitOfWork;
                _currentUser = currentUser;
                _reminderRepo = reminderRepo;
                _reminderService = reminderService;
            }

            public async Task<ReminderResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var initiator = _currentUser.GetMemberDetails();

                if (initiator is null || string.IsNullOrEmpty(initiator.ChandaNo))
                {
                    throw new NotFoundException($"Please login to update your reminder.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var reminder = await _reminderRepo.GetAsync(request.Id);
                if (reminder is null)
                {
                    throw new NotFoundException($"Reminder not found.", ExceptionCodes.ReminderNotFound.ToString(), 404);
                }

                reminder.UpdateStatus(request.TurnOn);
                reminder = await _reminderRepo.UpdateAsync(reminder);
                await _unitOfWork.SaveChangesAsync();

                if(request.TurnOn)
                {
                    _reminderService.ScheduleReminder(reminder);
                }
                else
                {
                    _reminderService.RemoveReminder(request.Id);
                }

                return reminder.Adapt<ReminderResponse>();
            }
        }
    }
}
