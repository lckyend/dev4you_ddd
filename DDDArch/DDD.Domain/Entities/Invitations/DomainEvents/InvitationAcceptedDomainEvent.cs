using DDD.Domain.Base;

namespace DDD.Domain.Entities.Invitations.DomainEvents;

public sealed record InvitationAcceptedDomainEvent(Guid InvitationId, Guid GatheringId) : IDomainEvent
{
    
}