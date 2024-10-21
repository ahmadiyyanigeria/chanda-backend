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
            private readonly IMemberRepository _memberRepo;
            private readonly IReminderRepository _reminderRepo;
            private readonly IReminderService _reminderService;

            public Handler(IUnitOfWork unitOfWork, ICurrentUser currentUser, IReminderRepository reminderRepo, IReminderService reminderService, IMemberRepository memberRepo)
            {
                _unitOfWork = unitOfWork;
                _memberRepo = memberRepo;
                _currentUser = currentUser;
                _reminderRepo = reminderRepo;
                _reminderService = reminderService;
            }

            public async Task<ReminderResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var initiator = _currentUser.GetMemberDetails();

                if (initiator is null || string.IsNullOrEmpty(initiator.ChandaNo))
                {
                    throw new NotFoundException($"Please login to create a reminder.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var member = _memberRepo.GetMember(initiator.Id);

                if (member is null)
                {
                    throw new NotFoundException($"Member not found.", ExceptionCodes.MemberNotFound.ToString(), 404);
                }

                var reminder = new Reminder(initiator.Id, request.Day, request.Hour, request.Minute, request.ReminderTitle, request.Description, request.ViaSMS, request.ViaMail);
                reminder.SetOwner(member);
                reminder = await _reminderRepo.AddAsync(reminder);
                await _unitOfWork.SaveChangesAsync();

                var dto = new ReminderDto
                {
                    Id = reminder.Id.ToString(),
                    Name = reminder.Member.Name,
                    Email = reminder.Member.Email,
                    PhoneNo = reminder.Member.PhoneNo,
                    CronExpression = reminder.CronExpression,
                    ReminderTitle = reminder.ReminderTitle,
                    Description = reminder.Description,
                    ViaMail = reminder.ViaMail,
                    ViaSMS = reminder.ViaSMS
                };
                _reminderService.ScheduleReminder(dto);

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
