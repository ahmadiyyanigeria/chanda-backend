using Application.Contracts;
using Application.Exceptions;
using Application.Repositories;
using Domain.Exceptions;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Commands.CreateReminder;

namespace Application.Commands
{
    public class DeleteReminder
    {
        public record Command(Guid Id): IRequest<string>;

        public class Handler : IRequestHandler<Command, string>
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

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var initiator = _currentUser.GetMemberDetails();

                if (initiator is null || string.IsNullOrEmpty(initiator.ChandaNo))
                {
                    throw new NotFoundException($"Please login to delete a reminder.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var reminder = await _reminderRepo.GetAsync(request.Id);
                if (reminder is null)
                {
                    throw new NotFoundException($"Reminder not found.", ExceptionCodes.ReminderNotFound.ToString(), 404);
                }

                _reminderRepo.DeleteReminder(reminder);
                await _unitOfWork.SaveChangesAsync();
                
                _reminderService.RemoveReminder(request.Id);
                
                return "Reminder deleted successfully.";
            }
        }
    }
}
