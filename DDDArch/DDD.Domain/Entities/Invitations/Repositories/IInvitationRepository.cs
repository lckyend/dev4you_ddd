using DDD.Domain.Entities.Gatherings;

namespace DDD.Domain.Entities.Invitations.Repositories;

public interface IInvitationRepository
{
    Task AddAsync(Invitation invitation);
}