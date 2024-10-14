using Application.Contracts;
using Application.DTOs;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Commands
{
    public class CreateReminder
    {
        public record Command: IRequest<ReminderResponse>
        {
            public int Day { get; init; }
            public int Hour { get; init; }
            public int Minute { get; init; }
            public string ReminderTitle { get; init; } = default!;
            public string? Description { get; init; }
            public bool ViaSMS { get; init; }
            public bool ViaMail { get; init; }
        }

        public record ReminderResponse(Guid Id, int Day, int Minute, int Hour, string ReminderTitle, string? Description, bool IsActive, bool ViaSMS, bool ViaMail);
        
        public class Handler: IRequestHandler<Command, ReminderResponse>
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
                var initiator = new MemberDetials { Name = "Ade Ola", ChandaNo = "0001", Email = "adeola@example.com", JamaatId = new Guid("5f9013da-5c0a-4af0-ae51-738f6bc0009d"), Roles = "Jamaat-President" };//_currentUser.GetMemberDetails();

                if (initiator is null || string.IsNullOrEmpty(initiator.ChandaNo))
                {
                    throw new NotFoundException($"Please login to create a reminder.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var reminder = new Reminder(initiator.Id, request.Day, request.Hour, request.Minute, request.ReminderTitle, request.Description, request.ViaSMS, request.ViaMail);
                reminder = await _reminderRepo.AddAsync(reminder);
                await _unitOfWork.SaveChangesAsync();

                _reminderService.ScheduleReminder(reminder);

                return reminder.Adapt<ReminderResponse>();
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Day)
                    .InclusiveBetween(1, 31)
                    .WithMessage("Invalid day of the month selected.");
                RuleFor(c => c.ReminderTitle)
                    .NotNull().NotEmpty().WithMessage("Reminder text is required.");
                RuleFor(c => c)
                    .Must(c => c.ViaSMS || c.ViaMail)
                    .WithMessage("At least one notification method (SMS or Mail) must be selected.");
            }
        }
    }
}
