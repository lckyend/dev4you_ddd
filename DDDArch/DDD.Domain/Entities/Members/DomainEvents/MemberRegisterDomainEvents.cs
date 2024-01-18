using DDD.Domain.Base;

namespace DDD.Domain.Entities.Members.DomainEvents;

public sealed record MemberRegisterDomainEvents(Guid MemberId) : IDomainEvent;