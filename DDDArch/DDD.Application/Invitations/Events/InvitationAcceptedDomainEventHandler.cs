using DDD.Application.Abstractions;
using DDD.Domain.Entities.Gatherings.Repositories;
using DDD.Domain.Entities.Invitations.DomainEvents;
using MediatR;

namespace DDD.Application.Invitations.Events;

internal sealed class InvitationAcceptedDomainEventHandler : INotificationHandler<InvitationAcceptedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IGatheringRepository _gatheringRepository;

    public InvitationAcceptedDomainEventHandler(
        IEmailService emailService, 
        IGatheringRepository gatheringRepository)
    {
        _emailService = emailService;
        _gatheringRepository = gatheringRepository;
    }

    public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
    {
        var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(
            notification.GatheringId, cancellationToken);
        
        if(gathering is null)
            return;
        
        await _emailService.SendInvitationAcceptedEmailAsync(gathering, cancellationToken);
    }
}