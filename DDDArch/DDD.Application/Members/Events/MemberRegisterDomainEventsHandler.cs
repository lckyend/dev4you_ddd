using DDD.Application.Abstractions;
using DDD.Domain.Entities.Members.DomainEvents;
using DDD.Domain.Entities.Members.Repositories;
using MediatR;

namespace DDD.Application.Members.Events;

internal sealed class MemberRegisterDomainEventsHandler : INotificationHandler<MemberRegisterDomainEvents>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IEmailService _emailService;

    public MemberRegisterDomainEventsHandler(IMemberRepository memberRepository, IEmailService emailService)
    {
        _memberRepository = memberRepository;
        _emailService = emailService;
    }

    public async Task Handle(MemberRegisterDomainEvents notification, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(notification.MemberId, cancellationToken);

        if (member is null)
            return;

        await _emailService.SendWelcomeEmailAsync(member, cancellationToken);
    }
}